using ReactiveUI;
using SportTracksXmlReader;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RunningTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadMapCommand = ReactiveCommand.Create(async () => await LoadBitmap());

            // TODO This is a short term hack to build the GPSRoute
            var rawData = "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";
            var gpsRoute = new GPSRoute
            {
                TrackData = new TrackData { Version = 4 }
            };
            gpsRoute.TrackData.Data = rawData;
            gpsRoute.DecodeBinaryData();

            var testLatitude = gpsRoute.LatitudeData[0];
            var testLongitude = gpsRoute.LongitudeData[0];

            MapPanels.Add(new StaticPanelViewModel(testLatitude, testLongitude));
/*
            var panels = MapPanelHelper.GetMapGridPanels(gpsRoute);
            for (int y = 0; y < panels.GetLength(1); y++) 
            {
                for (int x = 0; x < panels.GetLength(0); x++)
                {
                    MapPanels.Add(new MapPanelViewModel(panels[x, y].X, panels[x,y].Y));
                }
            }
*/
        }

        public ICommand LoadMapCommand { get; }

        public async Task LoadBitmap()
        {
            var bitmapLoad = new List<Task>();
            foreach(var panel in MapPanels)
            {
                bitmapLoad.Add(panel.LoadBitmap());
            }    
            
            await Task.WhenAll(bitmapLoad);
        }

        public ObservableCollection<StaticPanelViewModel> MapPanels { get; } = new();
    }
}
