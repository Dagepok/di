using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.Words_Preporation.FileReader;
using TagsCloudVisualization.Words_Preporation.Words_Checker;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualizator
    {
        public TagsCloudVisualizator(ICloudDrawer drawer, ITagCreator tagCreator, IFileReader reader,
            AbstractWordChecker wordChecker)
        {
            Drawer = drawer;
            TagCreator = tagCreator;
            Reader = reader;
            WordChecker = wordChecker;
        }

        private ICloudDrawer Drawer { get; }
        private ITagCreator TagCreator { get; }
        private IFileReader Reader { get; }
        private AbstractWordChecker WordChecker { get; }

        public void DrawCloud()
        {
            var words = WordChecker.GetSuitableWords(Reader.GetWords());
            var wordsFrequency = WordChecker.GetWordsFrequency(words);
            var tags = TagCreator.GetTags(wordsFrequency);
            Drawer.Draw(tags);
        }
    }
}