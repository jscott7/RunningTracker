using System.Collections.ObjectModel;

namespace RunningTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        { 
   
            MapPanels.Add(new MapPanelViewModel(0));
            MapPanels.Add(new MapPanelViewModel(1));

        }

        public ObservableCollection<MapPanelViewModel> MapPanels { get; } = new();
    }
}
