using System;
using System.Collections.Generic;
using ConjureGrade.Spells;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Interactors.Score;
using ConjureGrade.Wizards;

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
                CalculateGrade(newEvaluationEntity);
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

        private void CalculateGrade(EvaluationEntity evaluationToGrade)
        {
            var wizard = new EvaluationWizard();
            var scoreWizard = new ScoreWizard();

            var evalSpell = new EvaluationResult
            {
                TotalScoreCount = evaluationToGrade.NumberOfScores,
                DropLowest = evaluationToGrade.DropLowest,
                DropLowestCount = evaluationToGrade.NumberToDrop,
                PointValuePerScore = evaluationToGrade.PointsPerScore
            };

            var scores = ScoreInteractor.GetScoresByEvaluationId(evaluationToGrade.Id);

            var scoreSpells = new List<ScoreResult>();

            if (scores.Count > 0)
            {
                foreach (var scoreEntity in scores)
                {
                    scoreSpells.Add(scoreWizard.GetSingleScoreResult(scoreEntity.PointsEarned, scoreEntity.PointsPossible));
                }

                evalSpell.Scores = scoreSpells;

                wizard.Evaluation = evalSpell;

                wizard.UpdateAllGrades();

                evaluationToGrade.PointsEarned = wizard.Evaluation.PointsEarned;
                evaluationToGrade.CurrentPointsPossible = wizard.Evaluation.PointsPossibleToDate;
                evaluationToGrade.TotalPointsPossible = wizard.Evaluation.PointsPossibleOverall;
                evaluationToGrade.CurrentPointsGrade = wizard.Evaluation.GradeToDateRaw;
                evaluationToGrade.FinalPointsGrade = wizard.Evaluation.GradeOverallRaw;
            }
            else
            {
                var pointsTotal = (evaluationToGrade.NumberOfScores -
                                   evaluationToGrade.NumberToDrop) * evaluationToGrade.PointsPerScore;

                evaluationToGrade.CurrentPointsGrade = 1;
                evaluationToGrade.FinalPointsGrade = 0;
                evaluationToGrade.PointsEarned = 0;
                evaluationToGrade.CurrentPointsPossible = pointsTotal;
                evaluationToGrade.TotalPointsPossible = pointsTotal;
            }
            
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

                CalculateGrade(entityToUpdate);

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
                return new List<IDomainModel> { new ErrorDomainModel(gte, false) };
            }

            entityList = EvaluationInteractor.GetEvaluationsByCourseId(courseId);

            return ConvertToDomainModel(entityList);
        }

        public void ScoresUpdated(Guid evalId)
        {
            var eval = EvaluationInteractor.GetEvaluation(evalId);

            CalculateGrade(eval);

            EvaluationInteractor.UpdateEvaluation(eval);

            Courses.EvaluationModified(eval.CourseId);
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
                PointsPerScore = createModel.PointsPerScore,
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
                PointsPerScore = domainModel.PointsPerScore,
                DropLowest = domainModel.DropLowest
            };
        }
    }
}