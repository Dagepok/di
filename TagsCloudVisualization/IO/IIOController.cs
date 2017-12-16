namespace TagsCloudVisualization.IO
{
    public interface IIoController
    {
        Settings.Settings Settings { get; }
        void Output(string value);
        string Input();
    }
}