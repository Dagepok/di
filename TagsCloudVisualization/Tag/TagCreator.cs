using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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

        public List<Tag> GetTags(Dictionary<string, int> wordFrequency)
        {
            var maxFrequency = wordFrequency.Values.Max();
            return wordFrequency.Select(word => GetTag(word.Key, word.Value, maxFrequency)).ToList();
        }

        private int GetFontSize(int frequency, double maxFrequency)
        {
            return (int) (frequency / maxFrequency * (tagSettings.MaxFontSize - tagSettings.MinFontSize) +
                          tagSettings.MinFontSize);
        }

        private Tag GetTag(string word, int frequency, int maxFrequency)
        {
            var fontSize = GetFontSize(frequency, maxFrequency);
            var font = new Font(tagSettings.FontName, fontSize);
            var size = TextRenderer.MeasureText(word, font);
            var rect = layouter.PutNextRectangle(size);
            return new Tag(rect, tagSettings.FontColors[frequency % tagSettings.FontColors.Length], word, font);
        }
    }
}