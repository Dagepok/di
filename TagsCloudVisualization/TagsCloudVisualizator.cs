using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.IO;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.Words_Preporation;
using TagsCloudVisualization.Words_Preporation.FileReader;
using TagsCloudVisualization.Words_Preporation.WordConverter;
using TagsCloudVisualization.Words_Preporation.Words_Filters;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualizator
    {
        public TagsCloudVisualizator(ICloudDrawer drawer, ITagCreator tagCreator, IFileReader reader,
            IWordFilter wordFilter, WordConverterComposition converters, WordsCounter wordsCounter, IIoController io)
        {
            Io = io;
            WordsCounter = wordsCounter;
            WordConverters = converters;
            Settings = io.Settings;
            Drawer = drawer;
            TagCreator = tagCreator;
            Reader = reader;
            WordFilter = wordFilter;
        }

        private IIoController Io { get; }

        private WordsCounter WordsCounter { get; }

        private Settings.Settings Settings { get; }
        private ICloudDrawer Drawer { get; }
        private ITagCreator TagCreator { get; }
        private IFileReader Reader { get; }
        private IWordFilter WordFilter { get; }
        private WordConverterComposition WordConverters { get; }

        public void DrawCloud()
        {
            Result.Of(() => Reader.GetWords())
                .Then(w => WordConverters.Convert(w.Value))
                .Then(w => WordFilter.GetSuitableWords(w.Value))
                .Then(w => WordsCounter.GetWordsFrequency(w.Value, Settings.WordCount))
                .Then(w => TagCreator.GetTags(w))
                .Then(t => Drawer.Draw(t.Value))
                .OnFail(x => Io.Output(x));
        }
    }
}