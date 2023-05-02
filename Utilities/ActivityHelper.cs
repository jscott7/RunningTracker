using GPSLib;
using SportTracksXmlReader;

namespace Utilities
{
    public static class ActivityHelper
    {
        public static Mercator GetTopLeft(GPSRoute gpsRoute)
        {
            var latitudeMinMax = Utils.GetMinMax(gpsRoute.LatitudeData);
            var longitudeMinMax = Utils.GetMinMax(gpsRoute.LongitudeData);
            if (latitudeMinMax.Max.HasValue && longitudeMinMax.Min.HasValue)
            {
                return new Mercator(latitudeMinMax.Max.Value, longitudeMinMax.Min.Value, 13);
            }
            else
            {
                throw new NullReferenceException("Unable to obtain values for Latitude or Longitude");
            }
        }

        public static Mercator GetBottomRight(GPSRoute gpsRoute)
        {
            var latitudeMinMax = Utils.GetMinMax(gpsRoute.LatitudeData);
            var longitudeMinMax = Utils.GetMinMax(gpsRoute.LongitudeData);
            if (latitudeMinMax.Min.HasValue && longitudeMinMax.Max.HasValue)
            {
                return new Mercator(latitudeMinMax.Min.Value, longitudeMinMax.Max.Value, 13);
            }
            else
            {
                throw new NullReferenceException("Unable to obtain values for Latitude or Longitude");
            }
        }
    }
}
