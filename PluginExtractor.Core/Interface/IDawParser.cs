using PluginExtractor.Core.Model;

namespace PluginExtractor.Core.Interface;

public interface IDawParser
{
    Project ExtractPlugins(string path);
}