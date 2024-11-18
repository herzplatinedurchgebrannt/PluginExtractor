using PluginExtractor.Core.Model;

namespace PluginExtractor.Core.Interface;

public interface IDawParser
{
    List<Plugin> ExtractPlugins(string path);
}