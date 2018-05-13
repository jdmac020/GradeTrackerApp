using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Scores.Models;

namespace GradeTrackerApp.Models.Score
{
    public class ScoreViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public Guid EvaluationId { get; set; }

        [DisplayName("Score Name")]
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        [DisplayName("Date Completed")]
        public string DisplayDate
        {
            get { return Date.ToShortDateString(); }
        }

        [DisplayName("Grade Percent")]
        public double PointsGrade { get; set; }

        [DisplayName("Points Possible")]
        public double PointsPossible { get; set; }

        [DisplayName("Points Earned")]
        public double PointsEarned { get; set; }

        [DisplayName("Last Updated")]
        public DateTime? LastModified { get; set; }

        [DisplayName("Created")]
        public DateTime CreatedOn { get; set; }

        public ScoreViewModel() { }

        public ScoreViewModel(ScoreDomainModel domainModel)
        {
            Id = domainModel.Id;
            Name = domainModel.Name;
            Date = domainModel.Date;
            EvaluationId = domainModel.EvaluationId;
            PointsPossible = domainModel.PointsPossible;
            PointsEarned = domainModel.PointsEarned;
            PointsGrade = domainModel.PointsGrade;
            LastModified = domainModel.LastModified;
            CreatedOn = domainModel.CreatedOn;
        }
    }
}