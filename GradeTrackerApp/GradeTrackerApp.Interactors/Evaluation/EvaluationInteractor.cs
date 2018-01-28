using System;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
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

        public Guid CreateEvaluation(EvaluationEntity newEvaluationEntity)
        {
            var existingEval = Repo.GetAll().FirstOrDefault(e => e.CourseId == newEvaluationEntity.CourseId && e.Name == newEvaluationEntity.Name);

            if (existingEval != null)
                throw new ObjectAlreadyExistsException($"An Evaluation named {newEvaluationEntity.Name} already exists for this Course.");

            if (string.IsNullOrEmpty(newEvaluationEntity.Name))
                throw new MissingInfoException($"The name for the Evaluation cannot be blank.");

            newEvaluationEntity.Id = Guid.NewGuid();

            return Repo.Create(newEvaluationEntity);

        }

        public EvaluationEntity GetEvaluationById(Guid evaluationId)
        {
            if (evaluationId.Equals(Guid.Empty))
                throw new ObjectNotFoundException($"There is no Evaluation with an Id of {evaluationId}");

            return Repo.GetById(evaluationId);
        }
    }
}