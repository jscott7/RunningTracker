using GPSLib;
using SportTracksXmlReader;

namespace Utilities
{
    public static class MapPanelHelper
    {
        public static MercatorPanel GetTopLeft(GPSRoute gpsRoute)
        {
            var latitudeMinMax = Utils.GetMinMax(gpsRoute.LatitudeData);
            var longitudeMinMax = Utils.GetMinMax(gpsRoute.LongitudeData);
            if (latitudeMinMax.Max.HasValue && longitudeMinMax.Min.HasValue)
            {
                return new MercatorPanel(latitudeMinMax.Max.Value, longitudeMinMax.Min.Value, 13);
            }
            else
            {
                throw new NullReferenceException("Unable to obtain values for Latitude or Longitude");
            }
        }

        public static MercatorPanel GetBottomRight(GPSRoute gpsRoute)
        {
            var latitudeMinMax = Utils.GetMinMax(gpsRoute.LatitudeData);
            var longitudeMinMax = Utils.GetMinMax(gpsRoute.LongitudeData);
            if (latitudeMinMax.Min.HasValue && longitudeMinMax.Max.HasValue)
            {
                return new MercatorPanel(latitudeMinMax.Min.Value, longitudeMinMax.Max.Value, 13);
            }
            else
            {
                throw new NullReferenceException("Unable to obtain values for Latitude or Longitude");
            }
        }
        public static MercatorPanel[,] GetMapGridPanels(MercatorPanel topLeft, MercatorPanel bottomRight)
        {
            var xDiff = bottomRight.X - topLeft.X;
            var yDiff = bottomRight.Y - topLeft.Y; 

            var grid = new MercatorPanel[xDiff + 1, yDiff + 1];

            for (var xPos = 0; xPos < xDiff + 1; xPos++)
            {
                for (var yPos = 0; yPos < yDiff + 1; yPos++)
                {
                    grid[xPos, yPos] = new MercatorPanel(topLeft.X + xPos, topLeft.Y + yPos);
                }
            }

            return grid;
        }

        public static MercatorPanel[,] GetMapGridPanels(GPSRoute gpsRoute)
        {
            var topLeft = MapPanelHelper.GetTopLeft(gpsRoute);
            var bottomRight = MapPanelHelper.GetBottomRight(gpsRoute);
            return MapPanelHelper.GetMapGridPanels(topLeft, bottomRight);
        }

        // TODO Implement this
        public static (float, float) GetCenterLatLong(GPSRoute gpsRoute)
        {
            return (0, 0);
        }
    }
}
