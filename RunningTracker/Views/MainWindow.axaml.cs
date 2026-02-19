using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using RunningTracker.Models;
using RunningTracker.ViewModels;
using System.Threading.Tasks;

namespace RunningTracker.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        private void Init()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public MainWindow()
        {
            Init();
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(d => d(ViewModel!.ShowImportActivitiesDialog.RegisterHandler(DoShowImportActivitiesDialog)));
        }

        private async Task DoShowDialogAsync(InteractionContext<SettingsWindowViewModel, SettingsData?> interaction)
        {
            // To use SettingsWindow as a dialog it needs to inherit ReactiveWindow<SettingsWindowViewModel>
            var dialog = new SettingsWindow();
            dialog.DataContext = interaction.Input;
            
            var result = await dialog.ShowDialog<SettingsData?>(this);
            interaction.SetOutput(result);
        }

        private async Task DoShowImportActivitiesDialog(InteractionContext<ImportActivitiesWindowViewModel, ImportedActivitesData?> interaction)
        {
            var dialog = new ImportActivitiesWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<ImportedActivitesData?>(this);
            if (result?.FitFilePath?.Length > 0)
            {
                var mainWindowViewModel = (MainWindowViewModel)DataContext!;
                mainWindowViewModel.LoadActivity(result.FitFilePath);
            }

            interaction.SetOutput(result);
        }
    }
}
