using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Models.Evaluation
{
    public class CreateEvaluationViewModel : IViewModel
    {
        public Guid CourseId { get; set; }

        public bool StraightPointsOnly { get; set; }

        public bool WeightedOnly { get; set; }

        [Required(ErrorMessage = "*Name is required*")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "*Weight is required*")]
        [RegularExpression(@"^[0-9]$|^[0-9][0-9]$|^(100)$", ErrorMessage = "*Must Be a Whole Number Between 0 and 100*")]
        public double Weight { get; set; }

        [DisplayName("Number of Scores")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "*Must Be a Whole Number*")]
        public int NumberOfScores { get; set; } = 1;

        [DisplayName("Point Value Per Score")]
        public double PointValuePerScore { get; set; }

        [DisplayName("Lowest Score Get Dropped?")]
        public bool DropLowest { get; set; }

        [DisplayName("How Many Scores to Drop?")]
        public int ScoresToDrop { get; set; }

        public CreateEvaluationViewModel() { }

        public CreateEvaluationViewModel(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}