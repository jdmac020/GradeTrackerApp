using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Evaluation
{
    public interface IEvaluationInteractor
    {
        Guid CreateEvaluation(EvaluationEntity newEvaluationEntity);
        EvaluationEntity GetEvaluation(Guid evaluationId);
        List<EvaluationEntity> GetByCourseId(Guid courseId);
    }
}