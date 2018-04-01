using System;

namespace GradeTrackerApp.Domain.Scores.Models
{
    public class CreateScoreDomainModel 
    {
        public double PointsEarned { get; set; }
        public double PointsPossible { get; set; }
        public DateTime Date { get; set; }
        public Guid EvaluationId { get; set; }
        public string Name { get; set; }
        
    }
}