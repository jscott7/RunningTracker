using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.ViewModels;
using System;

namespace RunningTracker.Views
{
    public partial class ImportActivitiesWindow : ReactiveWindow<ImportActivitiesWindowViewModel>
    {
        private void init()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public ImportActivitiesWindow()
        {
            init();

            // Subscribe to Reactive Commands
            this.WhenActivated(d => d(ViewModel!.LoadActivitiesCommand.Subscribe()));
            this.WhenActivated(d => d(ViewModel!.OkCommand.Subscribe()));
            this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe()));
        }
    }
}
