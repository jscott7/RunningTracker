using Avalonia.Media.Imaging;
using ReactiveUI;
using RunningTracker.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunningTracker.ViewModels
{
    public class MapPanelViewModel : ViewModelBase
    {
        private Bitmap? _mapPanel;
        private MapPanel _mapPanelModel;

        public MapPanelViewModel(int mapIndex)
        {
            LoadMapCommand = ReactiveCommand.Create(async () => await LoadBitmap(mapIndex));

            _mapPanelModel = new MapPanel();
        }

        public ICommand LoadMapCommand { get; }

        public Bitmap? MapPanel
        {
            get { return _mapPanel; }
            private set => this.RaiseAndSetIfChanged(ref _mapPanel, value);
        }

        public async Task LoadBitmap(int mapIndex)
        {
            // using declaration
            await using var imageStream = await _mapPanelModel.LoadMapPanel(mapIndex);
            MapPanel = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));         
        }
    }
}
