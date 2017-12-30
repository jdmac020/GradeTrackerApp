using System;

namespace GradeTrackerApp.EntityFramework.Entities
{
    public class SchoolEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Guid SchoolTypeId { get; set; }
    }
}