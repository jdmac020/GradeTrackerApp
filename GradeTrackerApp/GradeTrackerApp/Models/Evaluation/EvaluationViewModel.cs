using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Models.Evaluation
{
    public class EvaluationViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int NumberOfScores { get; set; }
        public double PointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public double CurrentPointsGrade { get; set; }
        public double FinalPointsGrade { get; set; }
        public bool DropLowest { get; set; }

        public EvaluationViewModel() { }

        public EvaluationViewModel(EvaluationDomainModel domainModel)
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