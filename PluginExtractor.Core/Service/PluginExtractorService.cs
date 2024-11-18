using PluginExtractor.Core.Interface;
using PluginExtractor.Core.Model;

namespace PluginExtractor.Core.Service;

public class PluginExtractorService
{
    private readonly IDawParser _dawParser;

    public PluginExtractorService(IDawParser dawParser)
    {
        _dawParser = dawParser;
    }

    public List<Plugin> ExtractPlugins(string path)
    {
        return _dawParser.ExtractPlugins(path);
    }
}