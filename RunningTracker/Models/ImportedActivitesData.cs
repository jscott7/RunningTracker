using ReactiveUI;

namespace RunningTracker.Models
{   
    /// <summary>
    /// Container for activities imported from the ImportActivities dialog
    /// </summary>
    public class ImportedActivitesData : ReactiveObject
    {
        public ImportedActivitesData()
        {
            FitFilePath = "";
        }

        public string FitFilePath;
    }
}
