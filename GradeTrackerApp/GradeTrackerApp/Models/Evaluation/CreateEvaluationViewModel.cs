using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Models.Evaluation
{
    public class CreateEvaluationViewModel
    {
        public Guid CourseId { get; set; }

        [Required(ErrorMessage = "*Name is required*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Weight is required*")]
        [RegularExpression(@"^[1-9]$|^[1-9][1-9]$|^(100)$", ErrorMessage = "*Must Be a Whole Number Between 1 and 100*")]
        //[RegularExpression(@"^([0 - 9]{1,2}){1}(\.[0-9]{1,2})?$ ", ErrorMessage = "*Must Be a Whole Number Between 0 and 100*")]
        public double Weight { get; set; }

        [DisplayName("Number of Scores")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "*Must Be a Whole Number*")]
        public int NumberOfScores { get; set; } = 1;

        [DisplayName("Lowest Score Get Dropped?")]
        public bool DropLowest { get; set; }

        public CreateEvaluationViewModel() { }

        public CreateEvaluationViewModel(Guid courseId)
        {
            CourseId = courseId;
        }
    }
}