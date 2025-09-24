using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RunningTracker.Models
{
    public class StaticMapPanel
    {
        private static HttpClient _httpClient = new();
        private float _latitude;
        private float _longitude;
        private int _zoom;

        /// <summary>
        /// Creates an instance of a StaticMapPanel
        /// </summary>
        /// <param name="latitude">Latitude of middle of panel</param>
        /// <param name="longitude">Longituge of middle of panel</param>
        /// <param name="zoom">Zoom of image. Higher numbers mean bigger zoom</param>
        public StaticMapPanel(float latitude, float longitude, int zoom)
        {
            _latitude = latitude;
            _longitude = longitude;
            _zoom = zoom;
        }

        public async Task<Stream> LoadMapData()
        {
            var apiKey = MapApi.GetApiKey(); 
            var data = await _httpClient.GetByteArrayAsync($"https://tile.thunderforest.com/static/outdoors/{_longitude},{_latitude},{_zoom}/1000x1000@2x.png?apikey={apiKey}");
            return new MemoryStream(data);
        }  
    }
}
