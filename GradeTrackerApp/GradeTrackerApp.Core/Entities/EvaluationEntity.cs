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
        public double Weight { get; set; }
        public int NumberOfScores { get; set; }
        public double PointsPerScore { get; set; }
        public double TotalPointsPossible { get; set; } // Calculated
        public double CurrentPointsPossible { get; set; } // Calculated
        public double PointsEarned { get; set; } // Calculated
        public double CurrentPointsGrade { get; set; } // Calculated
        public double FinalPointsGrade { get; set; } // Calculated
        public bool DropLowest { get; set; }
        public int NumberToDrop { get; set; }

        //public virtual List<ScoreEntity> Scores { get; set; }
    }
}