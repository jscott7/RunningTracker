namespace Utilities
{
    public class SettingsPersistence
    {
        private static readonly string _settingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RunningTracker",
            "appdata.txt");

        public static string ApiKey
        {
            get
            {
                if (File.Exists(_settingsPath))
                {
                    foreach (var line in File.ReadAllLines(_settingsPath))
                    {
                        var kvp = line.Split('=');

                        if (kvp.Length == 2 && kvp[0].Equals("APIKEY", StringComparison.OrdinalIgnoreCase))
                        {
                            return kvp[1];
                        }
                    }
                }

                return "API Key required";
            }

        }

        public static void SaveApiKey(string value)
        {
            if (File.Exists(_settingsPath))
            {
                var lines = new List<string>();
                foreach (var line in File.ReadAllLines(_settingsPath))
                {
                    var kvp = line.Split('=');

                    if (kvp.Length == 2 && kvp[0].Equals("APIKEY", StringComparison.OrdinalIgnoreCase))
                    {
                        lines.Add("APIKEY=" + value);
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }

                File.WriteAllLines(_settingsPath, lines);
            }
            else
            {
                var directory = Path.GetDirectoryName(_settingsPath);
                if (directory != null)
                {
                    _ = Directory.CreateDirectory(directory);
                    File.WriteAllText(_settingsPath, "APIKEY=" + value);
                }
                else
                {
                    throw new Exception("Unable to create settings directory");
                }
            }
        }
    }
}

