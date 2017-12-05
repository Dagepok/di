using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.Words_Preporation;
using TagsCloudVisualization.Words_Preporation.FileReader;
using TagsCloudVisualization.Words_Preporation.WordConverter;
using TagsCloudVisualization.Words_Preporation.Words_Filters;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualizator
    {
        public TagsCloudVisualizator(ICloudDrawer drawer, ITagCreator tagCreator, IFileReader reader,
            IWordFilter wordFilter, WordConverterComposition converters, Settings.Settings settings)
        {
            WordConverters = converters;
            Settings = settings;
            Drawer = drawer;
            TagCreator = tagCreator;
            Reader = reader;
            WordFilter = wordFilter;
        }

        private Settings.Settings Settings { get; }
        private ICloudDrawer Drawer { get; }
        private ITagCreator TagCreator { get; }
        private IFileReader Reader { get; }
        private IWordFilter WordFilter { get; }
        private WordConverterComposition WordConverters { get; }

        public void DrawCloud()
        {
            var words = Reader.GetWords();
            words = WordConverters.ConvertByAll(words);
            words = WordFilter.GetSuitableWords(words);
            var wordsFrequency = WordsCounter.GetWordsFrequency(words, Settings.WordCount);
            var tags = TagCreator.GetTags(wordsFrequency);
            Drawer.Draw(tags);
        }
    }
}