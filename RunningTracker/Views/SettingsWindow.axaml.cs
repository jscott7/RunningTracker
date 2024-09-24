using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.ViewModels;
using System;

namespace RunningTracker.Views
{
    public partial class SettingsWindow : ReactiveWindow<SettingsWindowViewModel>
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // Subscribe to Reactive Commands and close this Window if they have been triggered
            this.WhenActivated(d => d(ViewModel!.OkCommand.Subscribe(Close)));
            this.WhenActivated(d => d(ViewModel!.CancelCommand.Subscribe(Close)));
        }
    }
}
