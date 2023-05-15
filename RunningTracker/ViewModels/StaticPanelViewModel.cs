using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Drawing.Imaging;
//using System.Drawing;
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
            StaticMapPanel = await Task.Run(() => AvaloniaBitmapFromSystemDrawingBitmap(imageStream)); ;
        }

        private Bitmap AvaloniaBitmapFromSystemDrawingBitmap(Stream imageStream)
        {
            using var bitmapTmp = new System.Drawing.Bitmap(imageStream);

            var bitmapdata = bitmapTmp.LockBits(new System.Drawing.Rectangle(0, 0, bitmapTmp.Width, bitmapTmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var avaloniaBitmap = new Bitmap(Avalonia.Platform.PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul,
                bitmapdata.Scan0,
                new Avalonia.PixelSize(bitmapdata.Width, bitmapdata.Height),
                new Avalonia.Vector(96, 96),
                bitmapdata.Stride);

            bitmapTmp.UnlockBits(bitmapdata);
            
            return avaloniaBitmap;
        }
 
    }
}
