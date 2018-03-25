using System;

namespace GradeTrackerApp.Core.Entities
{
    public class InstructorEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}