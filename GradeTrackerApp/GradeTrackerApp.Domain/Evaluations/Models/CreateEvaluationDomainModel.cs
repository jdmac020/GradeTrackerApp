﻿using System;

namespace GradeTrackerApp.Domain.Evaluations.Models
{
    public class CreateEvaluationDomainModel
    {
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public double Weight { get; set; }
        public int NumberOfScores { get; set; }
        public double PointsPerScore { get; set; }
        public bool DropLowest { get; set; }
    }
}