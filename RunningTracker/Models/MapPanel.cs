using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RunningTracker.Models
{
    public class MapPanel
    {
        private static HttpClient _httpClient = new();
        private int _xPos;
        private int _yPos;
        private int _zoom;

        public MapPanel(int xPos, int yPos, int zoom)
        {
            _xPos = xPos;
            _yPos = yPos;
            _zoom = zoom;
        }

        public async Task<Stream> LoadMapData()
        {
            var apiKey = MapApi.GetApiKey();
            var data = await _httpClient.GetByteArrayAsync($"https://tile.thunderforest.com/landscape/{_zoom}/{_xPos}/{_yPos}.png?apikey={apiKey}");
            return new MemoryStream(data);
        }
    }}
