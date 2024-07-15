using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.ViewModels;

namespace RunningTracker.Views
{
    public partial class ImportActivitiesWindow : ReactiveWindow<SettingsWindowViewModel>
    {
        public ImportActivitiesWindow()
        {
            InitializeComponent();
            // Subscribe to Reactive Commands and close this Window if they have been triggered
            //this.WhenActivated(d => d(ViewModel!.OkCommand.Subscribe(Close)));
            //this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe(Close)));
        }
    }
}
