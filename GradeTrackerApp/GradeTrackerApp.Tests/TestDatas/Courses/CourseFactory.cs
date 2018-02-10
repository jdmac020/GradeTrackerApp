using GradeTrackerApp.Domain.Courses.Models;
using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class CourseFactory
    {
        public static CreateCourseDomainModel Create_CreateCourseDomainModel_ValidMinimum()
        {
            return new CreateCourseDomainModel
            {
                Name = "Intro to Physics",
                Department = "PHYS",
                StartDate = DateTime.Parse("1/15/2018"),
                EndDate = DateTime.Parse("5/28/2018"),
                Number = "1145",
                SchoolId = Guid.Empty,
                SemesterId = Guid.NewGuid(),
                Year = 2018
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum()
        {
            return new CourseEntity
            {
                Name = "Intro to Physics",
                Department = "PHYS",
                StartDate = DateTime.Parse("1/15/2018"),
                EndDate = DateTime.Parse("5/28/2018"),
                Number = "1145",
                SchoolId = Guid.Empty,
                SemesterId = Guid.NewGuid(),
                Year = 2018
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum(Guid courseId)
        {
            return new CourseEntity
            {
                Id = courseId,
                Name = "Intro to Physics",
                Department = "PHYS",
                StartDate = DateTime.Parse("1/15/2018"),
                EndDate = DateTime.Parse("5/28/2018"),
                Number = "1145",
                SchoolId = Guid.Empty,
                SemesterId = Guid.Empty,
                Year = 2018
            };
        }

        public static CreateCourseDomainModel CreateCCreateCourseDomainModel_Empty()
        {
            return new CreateCourseDomainModel();
        }
    }
}
