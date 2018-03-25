using System;

namespace GradeTrackerApp.Core.Entities
{
    public class GradeThresholdEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public double MinimumPoints { get; set; }
        public double MaximumPoints { get; set; }
    }
}