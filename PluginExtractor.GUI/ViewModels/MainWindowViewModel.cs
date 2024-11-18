using System.Collections.Generic;
using PluginExtractor.Core.Model;
using PluginExtractor.Core;
using PluginExtractor.Core.Interface;
using System.Collections.ObjectModel;

namespace PluginExtractor.GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IDawParser _dawParser;

    public ObservableCollection<Plugin> Plugins{ get;} = [];

    public MainWindowViewModel(IDawParser dawParser)
    {
        _dawParser = dawParser;
        LoadPlugins("/home/lx/Recording/Reaper/Fade_away_linux/FadeAway_Reaper.rpp");
    }

    public void LoadPlugins(string projectFilePath)
    {
        var plugins = _dawParser.ExtractPlugins(projectFilePath);
        Plugins.Clear();

        foreach (var plugin in plugins)
        {
            Plugins.Add(plugin);
        }
    }

    public string Greeting { get; } = "Welcome to Avalonia!";
}
