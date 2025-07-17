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

                if (secretValue?.APIKey != null)
                {
                    return secretValue.APIKey;
                }


                return SettingsPersistence.ApiKey; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to load secret: {ex.Message}");         
            }
        }
    }
}
