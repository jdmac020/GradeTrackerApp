using System.Collections.Generic;

namespace GradeTrackerApp.Models.Evaluation
{
    public class EvaluationListViewModel : List<EvaluationViewModel>
    {
        public EvaluationListViewModel(IEnumerable<EvaluationViewModel> evaluations)
        {
            foreach (var eval in evaluations)
            {
                Add(eval);
            }
            
        }
    }
}