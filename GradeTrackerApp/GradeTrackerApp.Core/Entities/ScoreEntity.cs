using System;

namespace GradeTrackerApp.Core.Entities
{
    public class ScoreEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid EvaluationId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double PointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public double PointsGrade { get; set; }
    }
}