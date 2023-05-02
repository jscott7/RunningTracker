using ReactiveUI;
using RunningTracker.Models;
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
            MapPanels.Add(new MapPanelViewModel(0));
            MapPanels.Add(new MapPanelViewModel(1));

        }
        public ICommand LoadMapCommand { get; }

        public async Task LoadBitmap()
        {
            var bitmapLoad = new List<Task>();

            int mapIndex = 0;
            foreach(var panel in MapPanels)
            {
                bitmapLoad.Add(panel.LoadBitmap(mapIndex++));
            }    
            
            await Task.WhenAll(bitmapLoad);
        }

        public ObservableCollection<MapPanelViewModel> MapPanels { get; } = new();
    }
}
