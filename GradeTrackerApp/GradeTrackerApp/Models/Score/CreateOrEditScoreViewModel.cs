using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GradeTrackerApp.Domain.Scores.Models;
using GradeTrackerApp.Models.Validators;

namespace GradeTrackerApp.Models.Score
{
    public class CreateOrEditScoreViewModel : IViewModel
    {
        public Guid Id { get; set; }
        
        [DisplayName("Score Name")]
        [Required(ErrorMessage = "*All Scores Must Have a Unique Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Date Is Required*")]
        [PastDate(ErrorMessage = "*Date Can't Be In the Future*")]
        [DisplayName("Date Completed")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public Guid EvaluationId { get; set; }

        [DisplayName("Points Earned")]
        public double PointsEarned { get; set; }

        [DisplayName("Points Possible")]
        public double PointsPossible { get; set; }

        public CreateOrEditScoreViewModel() { }
        
        public CreateOrEditScoreViewModel(Guid evaluationId)
        {
            EvaluationId = evaluationId;
        }
    }
}