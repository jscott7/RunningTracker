using GPSLib;
using NUnit.Framework;

namespace Test
{
    internal class GPSTest
    {
        [TestCase(51.477928, 0, 13, 4096, 2725)]
        public void LatLong_To_Mercator(double latitude, double longitude, int zoom, int x, int y)
        {
            var mercator = new Mercator(latitude, longitude, zoom); 
            Assert.That(mercator.X, Is.EqualTo(x)); 
            Assert.That(mercator.Y, Is.EqualTo(y));
        }
    }
}
