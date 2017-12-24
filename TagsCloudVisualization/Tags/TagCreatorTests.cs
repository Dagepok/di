using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayout;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Tags
{
    [TestFixture]
    public class TagCreatorTests
    {
        [SetUp]
        public void SetUp()
        {
            layouter = Substitute.For<ICloudLayouter>();
            layouter.PutNextRectangle(new Size(20, 20)).ReturnsForAnyArgs(new Rectangle(0, 0, 20, 20));
            creator = new TagCreator(layouter, new TagSettings(new Settings.Settings()));
            words = new Dictionary<string, int>
            {
                {"word", 1},
                {"otherWord", 2}
            };
        }

        private ICloudLayouter layouter;
        private ITagCreator creator;
        private Dictionary<string, int> words;

        [Test]
        public void TagCreatir_ShouldReturn_AllTagsFromWords()
        {
            var tags = creator.GetTags(words);

            tags.Value.Should().HaveCount(2);
        }

        [Test]
        public void TagCreator_ShouldGet_DifferentColors()
        {
            var tags = creator.GetTags(words);

            tags.Value[0].Color.Should().NotBe(tags.Value[1].Color);
        }

        [Test]
        public void TagCreator_ShouldGet_DifferentFontSize()
        {
            var tags = creator.GetTags(words);

            tags.Value[0].Font.Size.Should().NotBe(tags.Value[1].Font.Size);
        }

        [Test]
        public void TagCreator_ShouldGetTag_WithWordsInside()
        {
            var tags = creator.GetTags(words);

            tags.Value.Select(x => x.Word).Should().BeEquivalentTo(words.Keys);
        }
    }
}