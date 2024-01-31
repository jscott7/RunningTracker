using System;
using System.IO;
using Utilities;

namespace RunningTracker.Models
{
    internal class MapApi
    {
        public static string GetApiKey()
        {
            try
            {          
                var secretsAppsettingReader = new SecretsAppSettingsReader();
                var secretValue = secretsAppsettingReader.ReadSection<SecretValues>("SecretValues");

                if (secretValue?.APIKey != null)
                {
                    return secretValue.APIKey;
                }

                var appPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RunningTracker", 
                    "appdata.txt");

                var apiKey = "";
                if (File.Exists(appPath))
                {
                    foreach(var line in File.ReadAllLines(appPath))
                    {
                        var kvp = line.Split('=');

                        if (kvp.Length == 2 && kvp[0].Equals("APIKEY", StringComparison.OrdinalIgnoreCase))
                        {
                            apiKey = kvp[1];
                        }
                    }
                }

                return apiKey;     
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to load secret: {ex.Message}");         
            }
        }
    }
}
