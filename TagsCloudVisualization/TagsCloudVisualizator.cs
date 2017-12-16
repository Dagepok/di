using System;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.IO;
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
            var words = Reader.GetWords();
            if (!words.IsSuccess)
            {
                Io.Output(words.Error);
                return;
            }
            words = WordConverters.Convert(words.Value);
            if (!words.IsSuccess)
            {
                Io.Output(words.Error);
                return;
            }
            words = WordFilter.GetSuitableWords(words.Value);
            if (!words.IsSuccess)
            {
                Io.Output(words.Error);
                return;
            }
            var wordsFrequency = WordsCounter.GetWordsFrequency(words.GetValueOrThrow(), Settings.WordCount);
            var tags = TagCreator.GetTags(wordsFrequency);
            if (tags.IsSuccess)
                Drawer.Draw(tags.GetValueOrThrow());
            else Io.Output(tags.Error);
        }
    }
}