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

            ValidateNewEvaluation(newEvaluationEntity);

            return Repo.Create(newEvaluationEntity);

        }

        protected void ValidateNewEvaluation(EvaluationEntity newEvaluationEntity)
        {
            if (string.IsNullOrEmpty(newEvaluationEntity.Name))
                throw new MissingInfoException("Evaluation Must Have a Name.");
            if (newEvaluationEntity.CourseId.Equals(Guid.Empty))
                throw new MissingInfoException("Evaluation Must be Linked to a Course.");
            if (newEvaluationEntity.Weight.Equals(0))
                throw new MissingInfoException("Evaluation Must Have a Weight Value.");
            if (newEvaluationEntity.NumberOfScores < 0)
                throw new MissingInfoException("The Number of Scores for this Evaluation Cannot be Less than 0.");
        }

        public EvaluationEntity GetEvaluation(Guid evaluationId)
        {
            if (evaluationId.Equals(Guid.Empty))
                throw new ObjectNotFoundException("Requested Evaluation Does Not Exist.");

            return Repo.GetById(evaluationId);
        }
    }
}