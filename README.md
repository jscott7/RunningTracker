# RunningTracker
Desktop Application for tracking running data sourced from GPS watch.

I have been using SportTracks desktop client to manage all my running data obtained from Garmin GPS watches. 
Unfortunately this has changed to an online subscription model. I want to be able to keep using the data obtained over several years and add to it with new runs.

This is a good opportunity to experiment with using the Avalonia UI framework as well as the latest .NET versions.

# Technical 

## Adding a new dialog window

Example for SettingsWindow

### SettingsWindow.axaml
Window layout

### SettingsWindowViewModel.cs
Commands. e.g. Button Clicks

### SettingsData.cs (ReactiveObject)
Model for data used in settings window

### To open
Add the following to MainWindowViewModel.cs
```
public ICommand SettingsCommand { get; }`
public Interaction<SettingsWindowViewModel, Models.SettingsData?> ShowDialog { get; }
```

```
public MainWindowViewModel()
{
   ShowDialog = new Interaction<SettingsWindowViewModel, Models.SettingsData?>();
   SettingsCommand = ReactiveCommand.Create(async () => await OpenSettings());`

``` 

```
public async Task OpenSettings()
{
    var settings = new SettingsWindowViewModel();
    await ShowDialog.Handle(settings);  
}
```	