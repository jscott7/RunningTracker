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

            try
            {
                _logbook = Persistence.LoadLogbook(LogbookPath);

                if (_logbook == null) { return; }
                var sortedActivities = _logbook.Activities
                    .OrderBy(o => o.StartTime)
                    .Skip(_logbook.Activities.Length - 10)
                    .ToList();

                foreach (var activity in sortedActivities)
                {
                    ActivityDates.Add(activity.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                ActivityDates = new ObservableCollection<string>(ActivityDates.OrderByDescending(i => i));
            }
            catch (Exception e)
            {
                // TODO : Handle this
                Console.WriteLine(e);
            }
        }

        public ICommand LoadMapCommand { get; }

        public ICommand SettingsCommand { get; }

        public ICommand ImportActivitiesCommand { get; }
      
        public Interaction<SettingsWindowViewModel, Models.SettingsData?> ShowDialog { get; }

        public Interaction<ImportActivitiesWindowViewModel, Models.ImportedActivitesData?> ShowImportActivitiesDialog { get; }
  
        public async Task OpenSettings()
        {
            var settings = new SettingsWindowViewModel();
            await ShowDialog.Handle(settings);  
        }

        public async Task OpenImportActivities()
        {
            var importActivitesViewModel = new ImportActivitiesWindowViewModel();
            await ShowImportActivitiesDialog.Handle(importActivitesViewModel);
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
