using System;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;

namespace GradeTrackerApp.Domain.Evaluations.Service
{
    public interface IEvaluationService
    {
        EvaluationDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel);
        EvaluationDomainModel GetEvaluation(Guid courseId);
    }
}