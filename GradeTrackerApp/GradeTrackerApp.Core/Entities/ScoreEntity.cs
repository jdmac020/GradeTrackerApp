using System;

namespace GradeTrackerApp.Core.Entities
{
    public class ScoreEntity : EntityBase<Guid>
    {
        public Guid EvaluationId { get; set; }
        public DateTime Date { get; set; }
        public double PointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public double PointsGrade { get; set; }
    }
}