using System;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class AccessLevelEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}