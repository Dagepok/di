using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;
using TagsCloudVisualization.CloudDrawer;
using TagsCloudVisualization.CloudLayout;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals.LogarithmicalSpiral;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Words_Preporation.FileReader;
using TagsCloudVisualization.Words_Preporation.WordConverter;
using TagsCloudVisualization.Words_Preporation.Words_Filters;
using TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech;

namespace TagsCloudVisualization
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            var settings = new Settings.Settings(options);

            var cloud = GetVisualizator(settings);
            cloud.DrawCloud();
        }

        private static TagsCloudVisualizator GetVisualizator(Settings.Settings settings)
        {
            var container = new WindsorContainer();

            container.Register(Component.For<Settings.Settings>().Instance(settings));

            container.Register(Component.For<ICloudDrawer>().ImplementedBy<FileCloudDrawer>());
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<ISpiral>().ImplementedBy<LogarithmicSpiral>());
            container.Register(Component.For<IFileReader>().ImplementedBy<TxtReader>());
            container.Register(Component.For<ITagCreator>().ImplementedBy<TagCreator>());
            container.Register(Component.For<IWordFilter>().ImplementedBy<PartOfSpeechFilter>());

            var converters = new WordConverterComposition(new ToLowerCaseConverter());
            container.Register(Component.For<WordConverterComposition>().Instance(converters));

            container.Register(Component.For<TxtReaderSettings>().IsDefault());
            container.Register(Component.For<LogarithmicSpiralSettings>().IsDefault());
            container.Register(Component.For<TagsCloudVisualizator>());
            container.Register(Component.For<DrawerSettings>().IsDefault());
            container.Register(Component.For<TagSettings>().IsDefault());

            return container.Resolve<TagsCloudVisualizator>();
        }
    }
}