using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Models.Score
{
    public class ScoreViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }

        [DisplayName("Weight Percent")]
        public double Weight { get; set; }

        [DisplayName("Number of Scores")]
        public int NumberOfScores { get; set; }

        [DisplayName("Points Possible")]
        public double PointsPossible { get; set; }

        [DisplayName("Points Earned")]
        public double PointsEarned { get; set; }

        [DisplayName("Current Grade Percent")]
        public double CurrentPointsGrade { get; set; }

        [DisplayName("Final Grade Percent")]
        public double FinalPointsGrade { get; set; }

        [DisplayName("Drop Lowest Score?")]
        public bool DropLowest { get; set; }



        public ScoreViewModel() { }

        public ScoreViewModel(EvaluationDomainModel domainModel)
        {
            Id = domainModel.Id;
            Name = domainModel.Name;
            CourseId = domainModel.CourseId;
            Weight = domainModel.Weight;
            NumberOfScores = domainModel.NumberOfScores;
            PointsPossible = domainModel.PointsPossible;
            PointsEarned = domainModel.PointsEarned;
            CurrentPointsGrade = domainModel.CurrentPointsGrade;
            FinalPointsGrade = domainModel.FinalPointsGrade;
            DropLowest = domainModel.DropLowest;

        }
    }
}