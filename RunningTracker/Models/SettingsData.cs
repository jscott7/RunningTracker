﻿using ReactiveUI;

namespace RunningTracker.Models
{
    /// <summary>
    /// Container for results returned from settings dialog
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
