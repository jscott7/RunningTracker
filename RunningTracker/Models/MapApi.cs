using System;
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
                return secretValue.APIKey;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to load secret: {ex.Message}");
                return "0";
            }
        }
    }
}
