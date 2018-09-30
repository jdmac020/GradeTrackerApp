using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public interface ICourseService
    {
        IDomainModel CreateCourse(CreateOrEditCourseDomainModel createModel);
        IDomainModel GetCourse(Guid courseId);
        IDomainModel DeleteCourse(Guid courseId);
        IDomainModel UpdateCourse(CreateOrEditCourseDomainModel updatedModel);
        List<IDomainModel> GetCourses(Guid userId);
        void EvaluationModified(Guid courseId);
        CourseWeightDomainModel GetCourseWeightType(Guid courseId);
        CourseWhatIfDomainModel CalcWhatIfGrade(IEnumerable<EvaluationDomainModel> whatIfModels);
    }
}