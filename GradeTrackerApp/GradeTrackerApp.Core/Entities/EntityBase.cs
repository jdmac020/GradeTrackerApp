using System;
using GradeTrackerApp.Core.Utility;

namespace GradeTrackerApp.Core.Entities
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        public DateTime? LastModified
        {
            get { return _lastModified?.ToUniversalTime() ; }
            set { _lastModified = value; }
        }

        private DateTime _createdOn = DateTimeHelper.UtcNow;
        private DateTime? _lastModified;
    }
}