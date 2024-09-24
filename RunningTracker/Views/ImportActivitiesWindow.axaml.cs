using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.ViewModels;
using System;

namespace RunningTracker.Views
{
    public partial class ImportActivitiesWindow : ReactiveWindow<ImportActivitiesWindowViewModel>
    {
        public ImportActivitiesWindow()
        {
            InitializeComponent();
            // Subscribe to Reactive Commands
            //this.WhenActivated(d => d(ViewModel!.LoadActivitiesCommand.Subscribe(Close)));
            this.WhenActivated(d => d(ViewModel!.OkCommand.Subscribe(Close)));
            this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe(Close)));
        }
    }
}
