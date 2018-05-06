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
        IDomainModel DeleteEvaluation(Guid evaluationId);
        IDomainModel UpdateEvaluation(EvaluationDomainModel updatedEvaluationModel);
        IDomainModel GetEvaluation(Guid evaluationId);
        List<IDomainModel> GetEvaluationsForCourse(Guid courseId);
    }
}