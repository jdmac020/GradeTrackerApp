﻿namespace GradeTrackerApp.Domain.Shared
{
    public class DomainModel : IDomainModel
    {
        public string Name { get; set; }

        public DomainModel() { }

        public DomainModel(string name)
        {
            Name = name;
        }
    }
}