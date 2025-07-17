namespace Utilities
{
    public class SettingsPersistence
    {
        private static readonly string _settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RunningTracker",
            "appdata.txt");

        private static readonly string ApiSettingKey = "APIKEY";
        private static readonly string LogbookPathSettingKey = "LOGBOOKPATH";

        public static string LogbookPath
        {
            get
            {
                return GetSettingsValue(LogbookPathSettingKey);
            }
        }

        public static string ApiKey
        {
            get
            {
                return GetSettingsValue(ApiSettingKey);
            }
        }

        public static void SaveLogbookPath(string logbookPath)
        {
            SaveKeyValue(LogbookPathSettingKey, logbookPath);
        }

        public static void SaveApiKey(string apiKey)
        {
            SaveKeyValue(ApiSettingKey, apiKey);
        }

        private static void SaveKeyValue(string key, string value)
        {
            var valueToSave = $"{key}={value}";
            if (File.Exists(_settingsPath))
            {
                bool keyExists = false;
                var lines = new List<string>();
                foreach (var line in File.ReadAllLines(_settingsPath))
                {
                    var kvp = line.Split('=');

                    if (kvp.Length == 2 && kvp[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        // If the key already exists, update its value
                        keyExists = true;
                        lines.Add(valueToSave);
                    }
                    else
                    {
                        // Otherwise, keep the existing line
                        lines.Add(line);
                    }
                }
                // If the key does not exist, add it to the end of the file
                if (!keyExists)
                {
                    lines.Add(valueToSave);
                }

                File.WriteAllLines(_settingsPath, lines);
            }
            else
            {
                // If the file does not exist, create it with the key-value pair
                var directory = Path.GetDirectoryName(_settingsPath);
                if (directory != null)
                {
                    _ = Directory.CreateDirectory(directory);
                    File.WriteAllText(_settingsPath, valueToSave);
                }
                else
                {
                    throw new Exception($"Unable to create settings directory {_settingsPath}");
                }
            }
        }

        private static string GetSettingsValue(String key)
        {
            if (File.Exists(_settingsPath))
            {
                foreach (var line in File.ReadAllLines(_settingsPath))
                {
                    var kvp = line.Split('=');

                    if (kvp.Length == 2 && kvp[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return kvp[1];
                    }
                }
            }

            return $"{key} Key required";
        }

    }
}

