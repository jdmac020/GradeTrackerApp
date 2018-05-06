using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Evaluation
{
    public interface IEvaluationInteractor
    {
        Guid CreateEvaluation(EvaluationEntity newEvaluationEntity);
        void DeleteEvaluation(Guid evaluationId);
        void UpdateEvaluation(EvaluationEntity updatedEvaluation);
        EvaluationEntity GetEvaluation(Guid evaluationId);
        List<EvaluationEntity> GetEvaluationsByCourseId(Guid courseId);
    }
}