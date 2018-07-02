using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Evaluations.Models
{
    public class EvaluationDomainModel : DomainModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public double Weight { get; set; }
        public int NumberOfScores { get; set; }
        public double PointsEarned { get; set; } // Calculated
        public double CurrentPointsGrade { get; set; } // Calculated
        public double FinalPointsGrade { get; set; } // Calculated
        public bool DropLowest { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime CreatedOn { get; set; }
        public double PointsPerScore { get; set; }
        public double TotalPointsPossible { get; set; } // Calculated
        public double CurrentPointsPossible { get; set; } // Calculated
        public int NumberToDrop { get; set; }

        public EvaluationDomainModel() { }

        public EvaluationDomainModel(EvaluationEntity evaluationEntity)
        {
            Id = evaluationEntity.Id;
            Name = evaluationEntity.Name;
            CourseId = evaluationEntity.CourseId;
            Weight = evaluationEntity.Weight;
            PointsEarned = evaluationEntity.PointsEarned;
            PointsPerScore = evaluationEntity.PointsPerScore;
            CurrentPointsGrade = evaluationEntity.CurrentPointsGrade;
            FinalPointsGrade = evaluationEntity.FinalPointsGrade;
            CurrentPointsPossible = evaluationEntity.CurrentPointsPossible;
            TotalPointsPossible = evaluationEntity.TotalPointsPossible;
            NumberOfScores = evaluationEntity.NumberOfScores;
            DropLowest = evaluationEntity.DropLowest;
            NumberToDrop = evaluationEntity.NumberToDrop;
            LastModified = evaluationEntity.LastModified;
            CreatedOn = evaluationEntity.CreatedOn;
        }
    }
}