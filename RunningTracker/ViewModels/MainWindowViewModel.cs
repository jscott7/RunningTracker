using ReactiveUI;
using SportTracksXmlReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace RunningTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _selectedActivityDate;
        private readonly Logbook _logbook;

        private readonly string LogbookPath = @"History.logbook3";

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<SettingsWindowViewModel, Models.SettingsData?>();
            ShowImportActivitiesDialog = new Interaction<ImportActivitiesWindowViewModel, Models.ImportedActivitesData?>();

            LoadMapCommand = ReactiveCommand.Create(async () => await LoadBitmap());
            SettingsCommand = ReactiveCommand.Create(async () => await OpenSettings());
            ImportActivitiesCommand = ReactiveCommand.Create(async () => await OpenImportActivities());

            _logbook = Persistence.LoadLogbook(LogbookPath);
            foreach (var activity in _logbook.Activities.Skip(_logbook.Activities.Length - 10))
            {
                ActivityDates.Add(activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            ActivityDates = new ObservableCollection<string>(ActivityDates.OrderByDescending(i => i));          
        }

        public ICommand LoadMapCommand { get; }

        public ICommand SettingsCommand { get; }

        public ICommand ImportActivitiesCommand { get; }
      
        public Interaction<SettingsWindowViewModel, Models.SettingsData?> ShowDialog { get; }

        public Interaction<ImportActivitiesWindowViewModel, Models.ImportedActivitesData?> ShowImportActivitiesDialog { get; }
  
        public async Task OpenSettings()
        {
            var settings = new SettingsWindowViewModel();
            var result = await ShowDialog.Handle(settings);     
        }

        public async Task OpenImportActivities()
        {
            var importActivitesViewModel = new ImportActivitiesWindowViewModel();
            var result = await ShowImportActivitiesDialog.Handle(importActivitesViewModel);
        }

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
