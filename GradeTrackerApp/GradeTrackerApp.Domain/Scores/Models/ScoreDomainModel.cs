using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Scores.Models
{
    public class ScoreDomainModel : DomainModel
    {
        public Guid Id { get; set; }
        public Guid EvaluationId { get; set; }
        public DateTime Date { get; set; }
        public double PointsPossible { get; set; }
        public double PointsEarned { get; set; }
        public double PointsGrade { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime CreatedOn { get; set; }

        public ScoreDomainModel() { }

        public ScoreDomainModel(ScoreEntity scoreEntity)
        {
            Id = scoreEntity.Id;
            Name = scoreEntity.Name;
            EvaluationId = scoreEntity.EvaluationId;
            Date = scoreEntity.Date;
            PointsPossible = scoreEntity.PointsPossible;
            PointsEarned = scoreEntity.PointsEarned;
            PointsGrade = scoreEntity.PointsGrade;
            LastModified = scoreEntity.LastModified;
            CreatedOn = scoreEntity.CreatedOn;
        }
    }
}