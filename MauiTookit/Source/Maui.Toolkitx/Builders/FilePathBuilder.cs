namespace Maui.Toolkitx.Builders;
public class FilePathBuilder
{
    internal FilePathBuilder()
    {

    }

    string? _Header;
    List<string> _FilePathNodes = new();

    public bool SetBasicDirectory(string? directory = default)
    {
        if (string.IsNullOrWhiteSpace(directory))
        {
#if WINDOWS || ANDROID
            var inPath = Environment.ProcessPath;
            var inDirctory = Path.GetDirectoryName(inPath);
            _Header = inDirctory;
#elif MACCATALYST || IOS
            var inPath = Foundation.NSBundle.MainBundle.BundlePath;
            var inDirctory = Path.Combine(inPath, "Contents");
            _Header = inDirctory;
#endif
        }
        else
            _Header = directory;

        return true;
    }

    public bool AddNodeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        _FilePathNodes.Add(name);
        return true;
    }

    public string Build()
    {
        var path = Path.Combine(_FilePathNodes.ToArray());
        if (_Header is not null)
            path = Path.Combine(_Header, path);

        return path;
    }
}
