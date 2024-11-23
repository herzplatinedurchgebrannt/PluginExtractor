using PluginExtractor.Core.Model;
using PluginExtractor.Core.Interface;
using System.Text.RegularExpressions;

namespace PluginExtractor.Reaper;

public class ReaperParser : IDawParser
{

    // private static readonly Regex PluginRegex = new Regex(@"<([^ ]+) ""([^:]+): ([^""]+)""", RegexOptions.Compiled);
    private static readonly Regex PluginRegex = new Regex(@"<*""([^:]+): ([^(]+) \(([^)]*)\)", RegexOptions.Compiled);

    public Project ExtractPlugins(string? projectFilePath)
    {
        if (string.IsNullOrEmpty(projectFilePath))
        {
            throw new ArgumentException("Project file path is missing.", nameof(projectFilePath));
        }

        if (!File.Exists(projectFilePath))
        {
            throw new FileNotFoundException("Reaper project file not found.", projectFilePath);
        }

        var plugins = new HashSet<string>();
        var pluginList = new List<Plugin>();

        foreach (var line in File.ReadLines(projectFilePath))
        {
            var match = PluginRegex.Match(line);
            if (match.Success)
            {
                string type = match.Groups[1].Value;
                string name = match.Groups[2].Value;
                string manufacturer = match.Groups[3].Value;

                string pluginKey = $"{name}:{manufacturer}:{type}";

                if (plugins.Add(pluginKey))
                {
                    Console.WriteLine($"{name}:{manufacturer}:{type}");
                    pluginList.Add(new Plugin (name, manufacturer, type));
                }
            }
        }

        return new Project("Projectname", "Reaper", projectFilePath, pluginList);
    }
}

