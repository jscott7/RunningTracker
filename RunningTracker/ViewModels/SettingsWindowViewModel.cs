using ReactiveUI;
using RunningTracker.Models;
using System.Reactive;
using Utilities;

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
                if (_settingsData != null)
                {
                    SettingsPersistence.SaveApiKey(ApiKey);
                }

                return _settingsData;
            });

            _settingsData = new SettingsData(SettingsPersistence.ApiKey);
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
       
        public string ApiKey { 
            get {  return _settingsData.ApiKey; } 
            set {  this.RaiseAndSetIfChanged(ref _settingsData.ApiKey, value); }
        }
    }
}
