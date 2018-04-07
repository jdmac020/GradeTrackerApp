﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GradeTrackerApp.Models.Score
{
    public class CreateScoreViewModel
    {
        [DisplayName("Score Name")]
        [Required(ErrorMessage = "*All Scores Must Have a Unique Name*")]
        public string Name { get; set; }

        [DisplayName("Date Completed")]
        public DateTime Date { get; set; }

        public Guid EvaluationId { get; set; }

        [DisplayName("Points Earned")]
        public double PointsEarned { get; set; }

        [DisplayName("Points Possible")]
        public double PointsPossible { get; set; }

        public CreateScoreViewModel() { }
        
        public CreateScoreViewModel(Guid evaluationId)
        {
            EvaluationId = evaluationId;
        }
    }
}