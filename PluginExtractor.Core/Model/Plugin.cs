namespace PluginExtractor.Core.Model;

public class Plugin
{
    public Plugin(string name, string manufacturer, string type, string version = "0.0.0")
    {
        Name = name;
        Manufacturer = manufacturer;
        Type = type;
        Version = version;
    }

    public string Name { get; private set;} = string.Empty;
    public string Manufacturer { get; private set;} = string.Empty;
    public string Type { get; private set;} = string.Empty;
    public string Version { get; private set;} = string.Empty;
}
