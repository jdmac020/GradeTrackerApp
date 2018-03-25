using System.Collections.Generic;

namespace GradeTrackerApp.Models.Score
{
    public class ScoreListViewModel : List<ScoreViewModel>
    {
        public ScoreListViewModel(IEnumerable<ScoreViewModel> scores)
        {
            foreach (var score in scores)
            {
                Add(score);
            }
            
        }
    }
}