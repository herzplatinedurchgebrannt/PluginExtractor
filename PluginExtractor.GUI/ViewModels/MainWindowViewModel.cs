using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginExtractor.Core.Interface;
using PluginExtractor.Core.Model;

namespace PluginExtractor.GUI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        #region fields
        private readonly IDawParser _parser;
        private readonly IFileDialogService _fileDialogService;
        [ObservableProperty] private string? _fileText;
        #endregion

        #region constructors
        public MainWindowViewModel(IDawParser parser, IFileDialogService fileDialogService)
        {
            _parser = parser;
            _fileDialogService = fileDialogService;
            ProjectName = nameof(ProjectName);
            ProjectPath = nameof(ProjectPath);
        }
        #endregion

        #region properties
        private string? _projectFilePath;
        public string? ProjectPath
        {
            get => _projectFilePath;
            set => this.SetProperty(ref _projectFilePath, value);
        }

        private string? _projectName;
        public string? ProjectName
        {
            get => _projectName;
            set => this.SetProperty(ref _projectName, value);
        }

        public ObservableCollection<Plugin> Plugins { get; } = [];
        #endregion

        #region methods
        [RelayCommand]
        private async Task OpenFile(CancellationToken token)
        {
            ErrorMessages?.Clear();
            try
            {
                var file = await _fileDialogService.DoOpenFilePickerAsync();
                if (file is null) return;

                ProjectPath = file.TryGetLocalPath();

                Plugins.Clear();
                Project project = _parser.ExtractPlugins(ProjectPath);
                foreach (var plugin in project.Plugins)
                {
                    Plugins.Add(plugin);
                }
            }
            catch (Exception e)
            {
                ErrorMessages?.Add(e.Message);
            }
        }

        [RelayCommand]
        private async Task SaveFile()
        {
            ErrorMessages?.Clear();
            try
            {
                var file = await _fileDialogService.DoSaveFilePickerAsync();
                if (file is null) return;
                if (FileText?.Length <= 1024 * 1024 * 10)
                {
                    var stream = new MemoryStream(Encoding.Default.GetBytes(FileText));
                    await using var writeStream = await file.OpenWriteAsync();
                    await stream.CopyToAsync(writeStream);
                }
                else
                {
                    throw new Exception("File exceeded 10MB limit.");
                }
            }
            catch (Exception e)
            {
                ErrorMessages?.Add(e.Message);
            }
        }
        #endregion
    }
}
