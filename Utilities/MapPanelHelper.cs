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
            return new MercatorPanel(latitudeMinMax.Max, longitudeMinMax.Min, 13);
        }

        public static MercatorPanel GetBottomRight(GPSRoute gpsRoute)
        {
            var latitudeMinMax = Utils.GetMinMax(gpsRoute.LatitudeData);
            var longitudeMinMax = Utils.GetMinMax(gpsRoute.LongitudeData);

            return new MercatorPanel(latitudeMinMax.Min, longitudeMinMax.Max, 13);
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

        public static (float latMid, float longMid) GetMidLatLong(GPSRoute gpsRoute)
        {
            var latitudeMid = Utils.GetMid(gpsRoute.LatitudeData);
            var longitudeMid = Utils.GetMid(gpsRoute.LongitudeData);
  
            return (latitudeMid, longitudeMid);
        }
    }
}
