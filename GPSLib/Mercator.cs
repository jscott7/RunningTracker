using System;

namespace GPSLib
{
    public class Mercator
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Mercator(double latitude, double longitude, int zoom)
        {
            ConvertLatLongToMercator(latitude, longitude, zoom);
        }

        private void ConvertLatLongToMercator(double latitude, double longitude, int zoom)
        {
            // Google
            // https://developers.google.com/maps/documentation/javascript/examples/map-coordinates
            /* X = 256 * (0.5 + longitude / 360);
             var siny = Math.Sin((latitude * Math.PI) / 180);
             Y = 256 * (0.5 - Math.Log((1 + siny ) / (1 - siny)) / (4 * Math.PI));

             var scale = 1 << 3;
             X = Math.Floor((X * scale) );
             Y = Math.Floor((Y * scale));
            */
            X = long2tilex(longitude, zoom);
            Y = lat2tiley(latitude, zoom);
        }

        // https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#ECMAScript_.28JavaScript.2FActionScript.2C_etc..29
        int long2tilex(double lon, int z)
        {
            // (1 << z) will shift 1 by z bits to the left, so as Z increases the output will simply double, i.e.
            // 2, 4, 8, 16, 32, 64, 128  (i.e. it's 2^n)
            return (int)(Math.Floor((lon + 180.0) / 360.0 * (1 << z)));
        }

        int lat2tiley(double lat, int z)
        {
            return (int)Math.Floor((1 - Math.Log(Math.Tan(ToRadians(lat)) + 1 / Math.Cos(ToRadians(lat))) / Math.PI) / 2 * (1 << z));
        }

        double ToRadians(double x)
        {
            return x * Math.PI / 180;
        }
    }
}