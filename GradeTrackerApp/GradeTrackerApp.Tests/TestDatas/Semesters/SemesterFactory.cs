using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Scores.Models;

namespace GradeTrackerApp.Tests.TestDatas.Semesters
{
    public static class SemesterFactory
    {

        public static ScoreEntity Create_ScoreEntity_Empty()
        {
            return new ScoreEntity();
        }

        public static SemesterEntity Create_SemesterEntity_ValidMinimum()
        {
            return new SemesterEntity
            {
                Id = Guid.NewGuid(),
                Name = "Fall"
            };
        }
        
        public static SemesterEntity Create_SemesterEntity_ValidMinimums(Guid semesterId)
        {
            return new SemesterEntity
            {
                Id = semesterId,
                Name = "Fall"

            };
        }

        public static List<SemesterEntity> Create_SemesterEntity_ListOfAll()
        {
            return new List<SemesterEntity>
            {
                new SemesterEntity {Id = Guid.NewGuid(), Name = "Fall"},
                new SemesterEntity {Id = Guid.NewGuid(), Name =  "Spring"},
                new SemesterEntity {Id = Guid.NewGuid(), Name = "Summer"}
            };
        }

    }
}
