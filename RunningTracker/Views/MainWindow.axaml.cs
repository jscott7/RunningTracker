using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.Models;
using RunningTracker.ViewModels;
using System.Threading.Tasks;

namespace RunningTracker.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private async Task DoShowDialogAsync(InteractionContext<SettingsWindowViewModel, SettingsData?> interaction)
        {
            // To use SettingsWindow as a dialog it needs to inherit ReactiveWindow<SettingsWindowViewModel>
            var dialog = new SettingsWindow();
            dialog.DataContext = interaction.Input;
            
            var result = await dialog.ShowDialog<SettingsData?>(this);
            interaction.SetOutput(result);
        }
    }
}
