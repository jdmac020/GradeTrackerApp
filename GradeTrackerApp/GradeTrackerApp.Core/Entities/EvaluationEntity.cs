using System;
using System.Collections.Generic;

namespace GradeTrackerApp.Core.Entities
{
    /// <summary>
    /// A category of assessments, such as "Tests" or "Labs" or "Homework" that contains individual scores
    /// </summary>
    public class EvaluationEntity : EntityBase<Guid>
    {
        public Guid CourseId { get; set; }
        public Guid WeightId { get; set; }
        public int NumberOfScores { get; set; }
        public double PointsPossible { get; set; } // Calculated
        public double PointsEarned { get; set; } // Calculated
        public double CurrentPointsGrade { get; set; } // Calculated
        public double FinalPointsGrade { get; set; } // Calculated
        public bool DropLowest { get; set; }

        //public virtual List<ScoreEntity> Scores { get; set; }
    }
}