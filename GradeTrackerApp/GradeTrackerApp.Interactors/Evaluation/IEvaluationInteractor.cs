using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Evaluation
{
    public interface IEvaluationInteractor
    {
        Guid CreateEvaluation(EvaluationEntity newEvaluationEntity);
        EvaluationEntity GetEvaluationById(Guid evaluationId);
    }
}