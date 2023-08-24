using ReactiveUI;
using SportTracksXmlReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace RunningTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _selectedActivityDate;
        private readonly Logbook _logbook;

        public MainWindowViewModel()
        {
            LoadMapCommand = ReactiveCommand.Create(async () => await LoadBitmap());

            _logbook = Persistence.LoadLogbook(@"C:\temp\Jonathan's History.logbook3");
            foreach (var activity in _logbook.Activities.Skip(_logbook.Activities.Length - 10))
            {
                ActivityDates.Add(activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            ActivityDates = new ObservableCollection<string>(ActivityDates.OrderByDescending(i => i));          
        }

        public ICommand LoadMapCommand { get; }

        public async Task LoadBitmap()
        {
            MapPanels.Clear();
            if (_selectedActivityDate is string selectedActivityDate)
            {
                var selectedActivity = _logbook.Activities.First(a => a.StartTime == DateTime.Parse(selectedActivityDate));

                var gpsRoute = selectedActivity.GPSRoute;
                gpsRoute.DecodeBinaryData();
                MapPanels.Add(new StaticPanelViewModel(gpsRoute));

                var bitmapLoad = new List<Task>();
                foreach (var panel in MapPanels)
                {
                    bitmapLoad.Add(panel.LoadBitmap());
                }

                await Task.WhenAll(bitmapLoad);
            }
        }

        public string? SelectedActivityDate
        {
            get => _selectedActivityDate;
            set => this.RaiseAndSetIfChanged(ref _selectedActivityDate, value);
        }
        public ObservableCollection<StaticPanelViewModel> MapPanels { get; } = new();

        public ObservableCollection<string> ActivityDates { get; } = new();
    }
}
