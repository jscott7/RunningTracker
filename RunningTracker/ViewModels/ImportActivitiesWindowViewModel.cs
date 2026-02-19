using ReactiveUI;
using RunningTracker.Models;
using System.Reactive;
using System.Threading.Tasks;

namespace RunningTracker.ViewModels
{
    public class ImportActivitiesWindowViewModel : ViewModelBase
    {      
        private readonly ImportedActivitesData _importedActivitiesData;

        public ImportActivitiesWindowViewModel() {
            // Behaviours for OK and Cancel Button click
            CancelCommand = ReactiveCommand.Create(() =>
            {
                _importedActivitiesData?.Clear();
                return _importedActivitiesData;
            });

            OkCommand = ReactiveCommand.Create(() =>
            {
                return _importedActivitiesData;
            });

            LoadActivitiesCommand = ReactiveCommand.CreateFromTask(OpenFilePickerAsync);

            _importedActivitiesData = new ImportedActivitesData();
        }

        /// <summary>
        /// Command linked to OK button. Needs to be ReactiveCommand so it can be hooked into Window events.
        /// </summary>
        public ReactiveCommand<Unit, ImportedActivitesData?> OkCommand { get; }
      
        /// <summary>
        /// Command linked to Cancel button. Needs to be ReactiveCommand so it can be hooked into Window events.
        /// </summary>
        public ReactiveCommand<Unit, ImportedActivitesData?> CancelCommand { get; }

        /// <summary>
        /// Command linked to Load Activities button.
        /// </summary>
        public ReactiveCommand<Unit, string?> LoadActivitiesCommand { get; }

        private async Task<string?> OpenFilePickerAsync()
        {
            var window = App.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null;

            if (window?.StorageProvider == null)
                return null;

            var options = new Avalonia.Platform.Storage.FilePickerOpenOptions
            {
                Title = "Select Activity File to Import",
                AllowMultiple = false,
                FileTypeFilter =
                [
                    new Avalonia.Platform.Storage.FilePickerFileType("Activity Files")
                    {
                        Patterns = [ "*.gpx", "*.tcx", "*.fit" ]
                    }
                ]
            };

            var files = await window.StorageProvider.OpenFilePickerAsync(options);
            if (files != null && files.Count > 0)
            {
                FitFilePath = files[0].Path.LocalPath;           
            }

            return null;
        }

        public string FitFilePath
        {
            get { return _importedActivitiesData.FitFilePath; }
            set { this.RaiseAndSetIfChanged(ref _importedActivitiesData.FitFilePath, value); }
        }
    }
}
