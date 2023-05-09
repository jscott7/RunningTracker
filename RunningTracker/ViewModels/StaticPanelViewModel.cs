using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Threading.Tasks;

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
            StaticMapPanel = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 800));         
        }
    }
}
