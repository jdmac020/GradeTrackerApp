namespace GradeTrackerApp.Domain.Evaluations.Models
{
    public class EvaluationDomainModel
    {
        private object courseEntity;

        public EvaluationDomainModel(object courseEntity)
        {
            this.courseEntity = courseEntity;
        }
    }
}