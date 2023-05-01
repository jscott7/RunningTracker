using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Utilities;

namespace RunningTracker.Models
{
    public class MapPanel
    {
        private static HttpClient _httpClient = new();

        public async Task<Stream> LoadMapPanel(int mapIndex)
        {
            var apiKey = GetApiKey();
            var xPos = 4096 + mapIndex;
            var data = await _httpClient.GetByteArrayAsync($"https://tile.thunderforest.com/landscape/13/{xPos}/2725.png?apikey={apiKey}");
            return new MemoryStream(data);
        }

        private string GetApiKey()
        {
            try
            {
                var secretsAppsettingReader = new SecretsAppSettingsReader();
                var secretValue = secretsAppsettingReader.ReadSection<SecretValues>("SecretValues");
                return secretValue.APIKey;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to load secret: {ex.Message}");
                return "0";
            }
        }
    }
}
