﻿using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Interactors.Score;
using GradeTrackerApp.Tests.TestDatas.Scores;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockScoreInteractor : IScoreInteractor
    {
        public Guid CreateCourse(ScoreEntity domainModel)
        {
            if (string.IsNullOrEmpty(domainModel.Name))
            {
                throw new MissingInfoException();
            }
            else
            {
                return Guid.NewGuid();
            }
        }

        public ScoreEntity GetCourseById(Guid courseId)
        {
            return ScoreFactory.Create_ScoreEntity_ValidMinimum(courseId);
        }
    }
}