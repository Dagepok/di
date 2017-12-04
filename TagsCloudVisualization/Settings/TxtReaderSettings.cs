namespace TagsCloudVisualization.Settings
{
    public class TxtReaderSettings
    {
        public string SourcePath;

        public TxtReaderSettings(Settings settings) => SourcePath = settings.SourcePath;
    }
}