using ReactiveUI;

namespace RunningTracker.Models
{
    /// <summary>
    /// Container for results returned from settings dialog
    /// 
    /// TODO Add location of logbook
    /// </summary>
    public class SettingsData : ReactiveObject
    {
        public SettingsData(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey;
    }
}
