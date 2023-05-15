using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;

namespace RunningTracker.ViewModels
{
    public class StaticPanelViewModel : ViewModelBase
    {
        private Bitmap? _mapPanel;
        private StaticMapPanel _mapPanelModel;

        public StaticPanelViewModel(float latitude, float longitude, int zoom = 13)
        {
            _mapPanelModel = new StaticMapPanel(latitude, longitude, zoom);
        }

        public Bitmap? StaticMapPanel
        {
            get { return _mapPanel; }
            private set => this.RaiseAndSetIfChanged(ref _mapPanel, value);
        }

        public async Task LoadBitmap()
        {
            await using var imageStream = await _mapPanelModel.LoadMapData();

            //  StaticMapPanel = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 800));
            StaticMapPanel = GetMapImageWithRoute(imageStream);
        }

        private static Bitmap GetMapImageWithRoute(Stream imageStream)
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
        /// Placeholder for drawing a route onto 
        /// </summary>
        /// <param name="bitmap">Clean map</param>
        /// <returns>Original Bitmap with route overlaid</returns>
        /// <remarks>
        /// Create a copy of the bitmap to avoid the following error
        /// "A Graphics object cannot be created from an image that has an indexed pixel format"</remarks>
        private static System.Drawing.Bitmap DrawRoute(System.Drawing.Bitmap bitmap)
        {
            var editableBitmap = new System.Drawing.Bitmap(bitmap);
            var graphics = System.Drawing.Graphics.FromImage(editableBitmap);
            var pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 20);
            graphics.DrawEllipse(pen, new System.Drawing.Rectangle(500, 500, 500, 500));
            return editableBitmap;
        }
    }
}
