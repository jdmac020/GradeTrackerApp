using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Core.Models;
using GradeTrackerApp.EntityFramework.Entities;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockCreateCourse : ICreateCourse
    {
        public CourseEntity Execute(CreateCourseDomainModel domainModel)
        {
            if (string.IsNullOrEmpty(domainModel.Name))
            {
                throw new MissingInfoException();
            }
            else
            {
                return new CourseEntity
                {
                    Id = Guid.NewGuid(),
                    Name = domainModel.Name,
                    Department = domainModel.Department,
                    Number = domainModel.Number,
                    SchoolId = domainModel.SchoolId,
                    InstructorId = domainModel.InstructorId,
                    Year = domainModel.Year,
                    SemesterId = domainModel.SemesterId,
                    EndTime = domainModel.EndTime,
                    StartTime = domainModel.StartTime,
                    StartDate = domainModel.StartDate,
                    EndDate = domainModel.EndDate,
                    TotalPointsPossible = 0,
                    CurrentPointsPossible = 0,
                    PointsEarned = 0,
                    EvaluationCount = domainModel.EvaluationCount,
                    CurrentPointsGrade = 0,
                    FinalPointsGrade = 0,
                    CreatedOn = DateTime.Now,
                };
            }
        }
    }
}
