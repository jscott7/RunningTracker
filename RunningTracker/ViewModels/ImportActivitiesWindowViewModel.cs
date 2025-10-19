using ReactiveUI;
using RunningTracker.Models;
using System.Reactive;
using System.Threading.Tasks;

namespace RunningTracker.ViewModels
{
    public class ImportActivitiesWindowViewModel : ViewModelBase
    {
        // TODO Need to change this type, ultimately to a list of activities to import
        private SettingsData? _settingsData;

        public ImportActivitiesWindowViewModel() {
            // Behaviours for OK and Cancel Button click
            CancelCommand = ReactiveCommand.Create(() =>
            {
                return _settingsData;
            });

            OkCommand = ReactiveCommand.Create(() =>
            {
                return _settingsData;
            });

            LoadActivitiesCommand = ReactiveCommand.CreateFromTask(OpenFilePickerAsync);
        }

        /// <summary>
        /// Command linked to OK button. Needs to be ReactiveCommand so it can be hooked into Window events.
        /// For example Close
        /// </summary>
        public ReactiveCommand<Unit, SettingsData?> OkCommand { get; }
      
        /// <summary>
        /// Command linked to Cancel button. Needs to be ReactiveCommand so it can be hooked into Window events.
        /// For example Close
        /// </summary>
        public ReactiveCommand<Unit, SettingsData?> CancelCommand { get; }

        /// <summary>
        /// Command linked to Load Activities button.
        /// </summary>
        public ReactiveCommand<Unit, string?> LoadActivitiesCommand { get; }

        private async Task<string?> OpenFilePickerAsync()
        {
            var window = App.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
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
                return files[0].Path.LocalPath;
            }

            return null;
        }
    }
}
