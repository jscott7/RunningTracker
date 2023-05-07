using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Threading.Tasks;

namespace RunningTracker.ViewModels
{
    public class MapPanelViewModel : ViewModelBase
    {
        private Bitmap? _mapPanel;
        private MapPanel _mapPanelModel;

        public MapPanelViewModel(int xPos, int yPos, int zoom = 13)
        {
            _mapPanelModel = new MapPanel(xPos, yPos, zoom);
        }

        public Bitmap? MapPanel
        {
            get { return _mapPanel; }
            private set => this.RaiseAndSetIfChanged(ref _mapPanel, value);
        }

        public async Task LoadBitmap()
        {
            // using declaration
            await using var imageStream = await _mapPanelModel.LoadMapData();
            MapPanel = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));         
        }
    }
}
