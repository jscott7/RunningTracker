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

        public StaticPanelViewModel(GPSRoute gpsRoute, int zoom = 14)
        {
            _gpsRoute = gpsRoute;

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
        /// Placeholder for drawing a route onto the bitmap
        /// </summary>
        /// <param name="bitmap">Clean map</param>
        /// <returns>Original Bitmap with route overlaid</returns>
        /// <remarks>
        /// Create a copy of the bitmap to avoid the following error
        /// "A Graphics object cannot be created from an image that has an indexed pixel format"</remarks>
        private System.Drawing.Bitmap DrawRoute(System.Drawing.Bitmap bitmap)
        {
            var editableBitmap = new System.Drawing.Bitmap(bitmap);
            
            var width = editableBitmap.Width;
            var height = editableBitmap.Height;

            var graphics = System.Drawing.Graphics.FromImage(editableBitmap);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 10);
           
            var midPoint = MapPanelHelper.GetMidLatLong(_gpsRoute);
         
            var parallelMultiplier = Math.Cos(midPoint.latMid * Math.PI / 180);
            var degreesPerPixelX = 360 / Math.Pow(2, 14 + 8);
            var degreesPerPixelY = 360 / Math.Pow(2, 14 + 8) * parallelMultiplier;

            var center = new System.Drawing.Point(width/2, height/2);

            var lastPt = center;
            var lastLat = midPoint.latMid;
            var lastLon = midPoint.longMid;

            for (var routeIndex = 0; routeIndex < _gpsRoute.LatitudeData.Count; routeIndex++)
            {
                var lat = _gpsRoute.LatitudeData[routeIndex];
                var longitude = _gpsRoute.LongitudeData[routeIndex];

                var latDiff = (lastLat - lat) / degreesPerPixelY * 2;
                var lonDiff = (longitude - lastLon) / degreesPerPixelX * 2;
 
                if ((int)latDiff == 0 || (int)lonDiff == 0) continue;

                var pt = new System.Drawing.Point(lastPt.X + (int)lonDiff, lastPt.Y + (int)latDiff);
                if (routeIndex > 0)
                {
                    graphics.DrawLine(pen, lastPt, pt);
                }

                lastPt = pt;
                lastLat = lat;
                lastLon = longitude;
            }

            return editableBitmap;
        }
    }
}
