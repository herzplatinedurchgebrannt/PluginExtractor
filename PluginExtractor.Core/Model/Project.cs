namespace PluginExtractor.Core.Model;

public class Project{
    public Project(string name, string type, string path, List<Plugin> plugins, string version = "0.0.0")
    {
        Name = name;
        Type = type;
        Path = path;
        Plugins = plugins;
        Version = version;
    }

    public string Name { get; private set;} = string.Empty;
    public string Type { get; private set;} = string.Empty; 
    public string Version { get; private set;} = string.Empty;
    public string Path { get; private set;} = string.Empty;
    public List<Plugin> Plugins { get; private set;} = [];
}

