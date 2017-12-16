using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using ResultOf;
using TagsCloudVisualization.CloudLayout;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class TagCreator : ITagCreator
    {
        private readonly ICloudLayouter layouter;
        private readonly TagSettings tagSettings;

        public TagCreator(ICloudLayouter layouter, TagSettings tagSettings)
        {
            this.layouter = layouter;
            this.tagSettings = tagSettings;
        }

        public Result<List<Tag>> GetTags(Dictionary<string, int> wordFrequency)
        {
            var maxFrequency = wordFrequency.Values.Max();
            var tags = new List<Tag>();
            foreach (var word in wordFrequency)
            {
                var tag = GetTag(word.Key, word.Value, maxFrequency);
                if (!tag.IsSuccess) return Result.Fail<List<Tag>>(tag.Error);
                tags.Add(tag.Value);
            }
            return Result.Ok(tags);
        }

        private int GetFontSize(int frequency, double maxFrequency)
        {
            return (int)(frequency / maxFrequency * (tagSettings.MaxFontSize - tagSettings.MinFontSize) +
                          tagSettings.MinFontSize);
        }

        private Result<Tag> GetTag(string word, int frequency, int maxFrequency)
        {

            var fontSize = GetFontSize(frequency, maxFrequency);
            var font = Result.Of(() =>
            {
                var f = new Font(tagSettings.FontName, fontSize);
                return f.Name == tagSettings.FontName ? f : throw new ArgumentException("Setted Font does't exists");
            });
            if (!font.IsSuccess)
                return Result.Fail<Tag>(font.Error);

            var size = Result.Of(() => TextRenderer.MeasureText(word, font.Value));
            if (!size.IsSuccess)
                return Result.Fail<Tag>(size.Error);

            var rect = layouter.PutNextRectangle(size.Value);
            return !IsTagInWindow(rect)
                ? Result.Fail<Tag>("Size of image too low")
                : Result.Ok(new Tag(rect, tagSettings.FontColors[frequency % tagSettings.FontColors.Length], word, font.Value));
        }

        private bool IsTagInWindow(Rectangle rect) => rect.Right < tagSettings.RightBorder
                                                      && rect.Left > tagSettings.LeftBorder
                                                      && rect.Top > tagSettings.TopBorder
                                                      && rect.Bottom < tagSettings.BottomBorder;
    }
}