using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Interactors.Score;

namespace GradeTrackerApp.Domain.Evaluations.Service
{
    public class EvaluationService : IEvaluationService
    {
        #region Services

        public ICourseService Courses
        {
            get { return _courseService ?? (_courseService = new CourseService()); }
            set { _courseService = value; }
        }

        private ICourseService _courseService;

        #endregion

        #region Interactors


        public IEvaluationInteractor EvaluationInteractor
        {
            get { return _evaluationInteractor ?? (_evaluationInteractor = new EvaluationInteractor()); }
            set { _evaluationInteractor = value; }
        }

        private IEvaluationInteractor _evaluationInteractor;

        public IScoreInteractor ScoreInteractor
        {
            get { return _scoreInteractor ?? (_scoreInteractor = new ScoreInteractor()); }
            set { _scoreInteractor = value; }
        }

        private IScoreInteractor _scoreInteractor;

        #endregion

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="evaluationInteractor">Interactor to be used by class</param>
        public EvaluationService(IEvaluationInteractor evaluationInteractor)
        {
            _evaluationInteractor = evaluationInteractor;
        }

        public EvaluationService()
        {

        }

        public IDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel)
        {
            var evaluationModel = new EvaluationDomainModel();
            var newEvaluationEntity = ConvertModelToEntity(createModel);

            var evaluationId = Guid.Empty;

            try
            {
                evaluationId = EvaluationInteractor.CreateEvaluation(newEvaluationEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            try
            {
                evaluationModel = (EvaluationDomainModel)GetEvaluation(evaluationId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            return evaluationModel;

        }

        public IDomainModel DeleteEvaluation(Guid evaluationId)
        {
            var deletedEvaluationModel = new EvaluationDomainModel();

            try
            {
                var evaluation = EvaluationInteractor.GetEvaluation(evaluationId);

                var linkedScores = ScoreInteractor.GetScoresByEvaluationId(evaluationId);

                if (linkedScores.Count > 0)
                {
                    foreach (var score in linkedScores)
                    {
                        ScoreInteractor.DeleteScore(score.Id);
                    }
                }

                EvaluationInteractor.DeleteEvaluation(evaluationId);

                deletedEvaluationModel = new EvaluationDomainModel() { CourseId = evaluation.CourseId };

            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            return deletedEvaluationModel;
        }

        public IDomainModel UpdateEvaluation(EvaluationDomainModel updatedEvaluationModel)
        {
            var returnModel = new EvaluationDomainModel();

            try
            {
                var entityToUpdate = ConvertModelToEntity(updatedEvaluationModel);
                EvaluationInteractor.UpdateEvaluation(entityToUpdate);

                var updatedEntity = EvaluationInteractor.GetEvaluation(entityToUpdate.Id);

                returnModel = new EvaluationDomainModel(updatedEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            return returnModel;
        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="evaluationId"></param>
        /// <returns></returns>
        public IDomainModel GetEvaluation(Guid evaluationId)
        {
            var evaluationModel = new EvaluationDomainModel();
            var evaluationEntity = new EvaluationEntity();

            try
            {
                evaluationEntity = EvaluationInteractor.GetEvaluation(evaluationId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            evaluationModel = new EvaluationDomainModel(evaluationEntity);

            return evaluationModel;
        }

        public List<IDomainModel> GetEvaluationsForCourse(Guid courseId)
        {
            var entityList = new List<EvaluationEntity>();

            try
            {
                entityList = EvaluationInteractor.GetEvaluationsByCourseId(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> { new ErrorDomainModel(gte, false)};
            }

            entityList = EvaluationInteractor.GetEvaluationsByCourseId(courseId);

            return ConvertToDomainModel(entityList);
        }

        public void UpdateLastModifiedDate(Guid evalId)
        {
            var eval = EvaluationInteractor.GetEvaluation(evalId);
            EvaluationInteractor.UpdateEvaluationLastModified(evalId);
            Courses.UpdateCourseLastModified(eval.CourseId);
        }

        protected List<IDomainModel> ConvertToDomainModel(List<EvaluationEntity> entities)
        {
            var modelList = new List<IDomainModel>();

            foreach (var eval in entities)
            {
                modelList.Add(new EvaluationDomainModel(eval));
            }

            return modelList;
        }

        private static EvaluationEntity ConvertModelToEntity(CreateEvaluationDomainModel createModel)
        {
            return new EvaluationEntity
            {
                Name = createModel.Name,
                CourseId = createModel.CourseId,
                Weight = createModel.Weight,
                NumberOfScores = createModel.NumberOfScores,
                DropLowest = createModel.DropLowest
            };
        }

        private static EvaluationEntity ConvertModelToEntity(EvaluationDomainModel domainModel)
        {
            return new EvaluationEntity
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                CourseId = domainModel.CourseId,
                Weight = domainModel.Weight,
                NumberOfScores = domainModel.NumberOfScores,
                DropLowest = domainModel.DropLowest
            };
        }
    }
}