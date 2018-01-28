using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Evaluation
{
    public class EvaluationInteractor : IEvaluationInteractor
    {
        private IRepository<EvaluationEntity, Guid> Repo
        {
            get { return _evaluationRepository ?? (_evaluationRepository = new Repository<EvaluationEntity, Guid>()); }
            set { _evaluationRepository = value; }
        }

        private IRepository<EvaluationEntity, Guid> _evaluationRepository;

        /// <summary>
        /// Test constructor for repository injection
        /// </summary>
        /// <param name="repo"></param>
        public EvaluationInteractor(IRepository<EvaluationEntity, Guid> repo)
        {
            _evaluationRepository = repo;
        }

        public EvaluationInteractor() { }

        public Guid CreateEvaluation(EvaluationEntity newCourseEntity)
        {
            throw new NotImplementedException();
        }

        public EvaluationEntity GetEvaluationById(Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}