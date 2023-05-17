using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using SportTracksXmlReader;

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
            var panelCenterLatitude = gpsRoute.LatitudeData[0];
            var panelCenterLongitude = gpsRoute.LongitudeData[0];
            _mapPanelModel = new StaticMapPanel(panelCenterLatitude, panelCenterLongitude, zoom);
        }

        public Bitmap? StaticMapPanel
        {
            get { return _mapPanel; }
            private set => this.RaiseAndSetIfChanged(ref _mapPanel, value);
        }

        public async Task LoadBitmap()
        {
            await using var imageStream = await _mapPanelModel.LoadMapData();

            // StaticMapPanel = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 800));
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

            double scale = 2650.0 / 1000.0 ;

            var totalLong = 0.022 * scale;
            var totalLat = 0.022 * scale;

            var centerLat = _gpsRoute.LatitudeData[0]; 
            var centerLon = _gpsRoute.LongitudeData[0]; 

            var leftLon = centerLon - totalLong / 2; 
            var rightLon = centerLon + totalLong / 2; 
            var topLat = centerLat + totalLat / 2; 
            var bottomLat = centerLat - totalLat / 2;

            var latPerPixel = (topLat - centerLat) / 500;
            var lonPerPixel = (leftLon - centerLon) / 500;


            var center = new System.Drawing.Point(width/2, height/2);

            var lastPt = center;
            var lastLat = _gpsRoute.LatitudeData[0];
            var lastLon = _gpsRoute.LongitudeData[0];
            for (var routeIndex = 1; routeIndex < _gpsRoute.LatitudeData.Count; routeIndex++)
            {
                var lat = _gpsRoute.LatitudeData[routeIndex];
                var longitude = _gpsRoute.LongitudeData[routeIndex];

                var latDiff = (lastLat - lat) / latPerPixel;
                var lonDiff = (lastLon - longitude) / lonPerPixel;
                if ((int)latDiff == 0 || (int)lonDiff == 0) continue;
 
                var pt = new System.Drawing.Point(lastPt.X + (int)lonDiff, lastPt.Y + (int)latDiff);
                graphics.DrawLine(pen, lastPt, pt);
                lastPt = pt;
                lastLat = lat;
                lastLon = longitude;
            }

     //       graphics.DrawEllipse(pen, new System.Drawing.Rectangle(width/2, height/2, 500, 500));
            return editableBitmap;
        }
    }
}
