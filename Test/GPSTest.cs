using GPSLib;
using NUnit.Framework;
using System.Collections.Generic;

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

        [Test]
        public static void MinMax_From_LatLongList()
        {
            var lat = new List<float> { 52.1f, 55.1f, 58.1f };
            var minMax = Utils.GetMinMax(lat);

            Assert.That(minMax.Min, Is.EqualTo(52.1f));
            Assert.That(minMax.Max, Is.EqualTo(58.1f));
        }

        [Test]
        public static void MinMax_From_LatLongList_WithDuplicate()
        {
            var lat = new List<float> { 52.1f, 52.1f, 55.1f, 58.1f };
            var minMax = Utils.GetMinMax(lat);

            Assert.That(minMax.Min, Is.EqualTo(52.1f));
            Assert.That(minMax.Max, Is.EqualTo(58.1f));

            var lat2 = new List<float> { 52.1f, 55.1f, 55.1f, 58.1f };
            var minMax2 = Utils.GetMinMax(lat2);

            Assert.That(minMax.Min, Is.EqualTo(52.1f));
            Assert.That(minMax.Max, Is.EqualTo(58.1f));
        }

        [Test]
        public static void MinMax_From_EmptyList()
        {
            var lat = new List<float> ();
            var minMax = Utils.GetMinMax(lat);

            Assert.That(minMax.Min.HasValue, Is.False);
            Assert.That(minMax.Max.HasValue, Is.False);
        }
    }
}
