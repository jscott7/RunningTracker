using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

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
            // TODO add ApiKey management
            return "12345";
        }
    }
}
