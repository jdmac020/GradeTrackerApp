using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Domain.Evaluations.Service
{
    public interface IEvaluationService
    {
        IDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel);
        IDomainModel GetEvaluation(Guid evaluationId);
        List<EvaluationDomainModel> GetEvaluationsForCourse(Guid courseId);
    }
}