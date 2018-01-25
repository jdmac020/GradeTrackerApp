using System;

namespace GradeTrackerApp.Core.Entities
{
    public class SemesterEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}