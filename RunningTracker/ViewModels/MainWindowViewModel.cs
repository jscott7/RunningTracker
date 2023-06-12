using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace RunningTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadMapCommand = ReactiveCommand.Create(async () => await LoadBitmap());

            var logbook = Persistence.LoadLogbook(@"C:\temp\cutdownlogbook.logbook3");
            var gpsRoute = logbook.Activities[logbook.Activities.Length - 1].GPSRoute;
            gpsRoute.DecodeBinaryData();
            MapPanels.Add(new StaticPanelViewModel(gpsRoute));
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
