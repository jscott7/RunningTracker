using ReactiveUI;
using RunningTracker.Models;
using System.Reactive;

namespace RunningTracker.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private SettingsData? _settingsData;

        public SettingsWindowViewModel() {

            // Behaviours for OK and Cancel Button click
            CancelCommand = ReactiveCommand.Create(() => 
            {            
                return _settingsData;
            });

            OkCommand = ReactiveCommand.Create(() =>
            {                  
                return _settingsData;
            });
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
    }
}
