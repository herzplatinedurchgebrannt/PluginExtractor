using PluginExtractor.Core.Interface;
using PluginExtractor.Core.Model;

namespace PluginExtractor.Core.Service;

public class PluginExtractorService(IDawParser dawParser)
{
    private readonly IDawParser _dawParser = dawParser;

    public Project ExtractPlugins(string path)
    {
        return _dawParser.ExtractPlugins(path);
    }
}