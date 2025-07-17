using ReactiveUI;

namespace RunningTracker.Models
{
    /// <summary>
    /// Container for results returned from settings dialog
    /// </summary>
    public class SettingsData : ReactiveObject
    {
        public SettingsData(string apiKey, string logbookPath)
        {
            ApiKey = apiKey;
            LogbookPath = logbookPath;
        }

        public string ApiKey;

        public string LogbookPath;
    }
}
