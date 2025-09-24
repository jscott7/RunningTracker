using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using SportTracksXmlReader;
using Utilities;
using System;

namespace RunningTracker.ViewModels
{
    public class StaticPanelViewModel : ViewModelBase
    {
        private Bitmap? _mapPanel;
        private StaticMapPanel _mapPanelModel;
        private GPSRoute _gpsRoute;
        private int _zoom;

        public StaticPanelViewModel(GPSRoute gpsRoute, int zoom = 14)
        {
            _gpsRoute = gpsRoute;
            _zoom = zoom;
            var midPoint = MapPanelHelper.GetMidLatLong(gpsRoute);
            _mapPanelModel = new StaticMapPanel(midPoint.latMid, midPoint.longMid, zoom);
        }

        public Bitmap? StaticMapPanel
        {
            get { return _mapPanel; }
            private set => this.RaiseAndSetIfChanged(ref _mapPanel, value);
        }

        public async Task LoadBitmap()
        {
            await using var imageStream = await _mapPanelModel.LoadMapData();
            StaticMapPanel = await Task.Run(() => GetMapImageWithRoute(imageStream));
        }

        private Bitmap GetMapImageWithRoute(Stream imageStream)
        {
            using var bitmapFromStream = new System.Drawing.Bitmap(imageStream);
            using var bitmapTmp = DrawRoute(bitmapFromStream);
            
            var bitmapdata = bitmapTmp.LockBits(new System.Drawing.Rectangle(0, 0, bitmapTmp.Width, bitmapTmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var avaloniaBitmap = new Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                bitmapdata.Scan0,
                new Avalonia.PixelSize(bitmapdata.Width, bitmapdata.Height),
                new Avalonia.Vector(96, 96),
                bitmapdata.Stride);

            bitmapTmp.UnlockBits(bitmapdata);
            
            return avaloniaBitmap;
        }

        /// <summary>
        /// Draws a route onto the bitmap
        /// </summary>
        /// <param name="bitmap">Clean map</param>
        /// <returns>Original Bitmap with route overlaid</returns>
        /// <remarks>
        /// Create a copy of the bitmap to avoid the following error
        /// "A Graphics object cannot be created from an image that has an indexed pixel format"</remarks>
        private System.Drawing.Bitmap DrawRoute(System.Drawing.Bitmap bitmap)
        {
            var editableBitmap = new System.Drawing.Bitmap(bitmap);
            
            var graphics = System.Drawing.Graphics.FromImage(editableBitmap);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 10);
           
            var gpsRouteInfo = MapPanelHelper.GetMidLatLong(_gpsRoute);
         
            var parallelMultiplier = Math.Cos(gpsRouteInfo.latMid * Math.PI / 180);
            var degreesPerPixelX = 360 / Math.Pow(2, _zoom + 8);
            var degreesPerPixelY = 360 / Math.Pow(2, _zoom + 8) * parallelMultiplier;

            var centerPt = new System.Drawing.Point(editableBitmap.Width / 2, editableBitmap.Height / 2);
            var centerLat = gpsRouteInfo.latMid;
            var centerLon = gpsRouteInfo.longMid;
            var lastPt = centerPt;

            for (var routeIndex = 0; routeIndex < _gpsRoute.LatitudeData.Count; routeIndex++)
            {
                var latitude = _gpsRoute.LatitudeData[routeIndex];
                var longitude = _gpsRoute.LongitudeData[routeIndex];

                var latPixel = (centerLat - latitude) / degreesPerPixelY * 2; // *2 because we called the scaling modifer in the API Call
                var lonPixel = (longitude - centerLon) / degreesPerPixelX * 2;

                var pt = new System.Drawing.Point(centerPt.X + (int)lonPixel, centerPt.Y + (int)latPixel);
            
                if (routeIndex > 0) // Don't draw first line from center
                {
                    graphics.DrawLine(pen, lastPt, pt);
                }

                lastPt = pt;
            }

            return editableBitmap;
        }
    }
}
