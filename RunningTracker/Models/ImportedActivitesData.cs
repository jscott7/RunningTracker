using ReactiveUI;

namespace RunningTracker.Models
{   
    /// <summary>
    /// Container for activities imported from the ImportActivities dialog
    /// </summary>
    public class ImportedActivitesData : ReactiveObject
    {
        public string FitFilePath;

        public ImportedActivitesData()
        {
            FitFilePath = "";
        }

        public void Clear()
        {
            FitFilePath = "";
        }
    }       
       
}
