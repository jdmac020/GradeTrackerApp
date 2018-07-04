using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Models.Score;

namespace GradeTrackerApp.Models.Evaluation
{
    public class EvaluationViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }

        [DisplayName("Weight Percent")]
        public double Weight { get; set; }

        [DisplayName("Number of Scores")]
        public int NumberOfScores { get; set; }

        [DisplayName("Points Value Per Score")]
        public double PointValuePerScore { get; set; }

        [DisplayName("Total Points Possible")]
        public double TotalPointsPossible { get; set; }

        [DisplayName("Current Points Possible")]
        public double CurrentPointsPossible { get; set; }

        [DisplayName("Points Earned")]
        public double PointsEarned { get; set; }

        [DisplayName("Current Grade Percent")]
        public double CurrentGrade { get; set; }

        [DisplayName("Final Grade Percent")]
        public double FinalGrade { get; set; }

        [DisplayName("Drop Lowest Score?")]
        public bool DropLowest { get; set; }

        [DisplayName("Number of Scores Dropped")]
        public int DropLowestCount { get; set; }

        public ScoreListViewModel Scores { get; set; }
        
        public DateTime? LastModified { get; set; }
        public DateTime CreatedOn { get; set; }

        public EvaluationListViewModel Evaluations { get; set; }

        public EvaluationViewModel() { }

        public EvaluationViewModel(EvaluationDomainModel domainModel)
        {
            Id = domainModel.Id;
            Name = domainModel.Name;
            CourseId = domainModel.CourseId;
            Weight = domainModel.Weight * 100;
            NumberOfScores = domainModel.NumberOfScores;
            PointValuePerScore = domainModel.PointsPerScore;
            TotalPointsPossible = domainModel.TotalPointsPossible;
            CurrentPointsPossible = domainModel.CurrentPointsPossible;
            PointsEarned = domainModel.PointsEarned;
            CurrentGrade = domainModel.CurrentPointsGrade *100;
            FinalGrade = domainModel.FinalPointsGrade * 100;
            DropLowest = domainModel.DropLowest;
            LastModified = domainModel.LastModified;
            CreatedOn = domainModel.CreatedOn;
        }
    }
}