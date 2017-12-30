using System;
using System.Collections.Generic;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class EvaluationEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public Guid WeightId { get; set; }
        public int NumberOfScores { get; set; }
        public virtual List<ScoreEntity> Scores { get; set; }
        public double PointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}