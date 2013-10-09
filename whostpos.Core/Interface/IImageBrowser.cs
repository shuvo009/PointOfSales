namespace whostpos.Core.Interface
{
    public interface IImageBrowser
    {
        byte[] GetFromFile();
        byte[] GetFromWebCam();

    }
}
