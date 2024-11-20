using System.Collections.Generic;
using PluginExtractor.Core.Model;
using PluginExtractor.Core;
using PluginExtractor.Core.Interface;
using System.Collections.ObjectModel;

namespace PluginExtractor.GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDawParser _dawParser;

    public string ProjectName { get; private set; } = string.Empty;
    public ObservableCollection<Plugin> Plugins{ get;} = [];

    public MainWindowViewModel(IDawParser dawParser)
    {
        _dawParser = dawParser;
        LoadPlugins("/home/lx/Recording/Reaper/Fade_away_linux/FadeAway_Reaper.rpp");
    }

    public void LoadPlugins(string projectFilePath)
    {
        var project = _dawParser.ExtractPlugins(projectFilePath);
        Plugins.Clear();

        foreach (Plugin plugin in project.Plugins)
        {
            Plugins.Add(plugin);
        }

        ProjectName = project.Name;
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}
