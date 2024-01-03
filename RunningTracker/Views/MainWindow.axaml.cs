using Avalonia.ReactiveUI;
using ReactiveUI;
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

        private async Task DoShowDialogAsync(InteractionContext<SettingsWindowViewModel, bool?> interaction)
        {
            var dialog = new SettingsWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<bool?>(this);
            interaction.SetOutput(result);
        }
    }
}
