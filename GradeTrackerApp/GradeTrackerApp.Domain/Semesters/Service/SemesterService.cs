using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Semester;

namespace GradeTrackerApp.Domain.Semesters.Service
{
    public class SemesterService : ISemesterService
    {
        #region Interactors

        public ISemesterInteractor Interactor
        {
            get { return _interactor ?? (_interactor = new SemesterInteractor());}
            set { _interactor = value; }
        }

        private ISemesterInteractor _interactor;

        #endregion

        public SemesterService() { }

        public SemesterService(ISemesterInteractor interactor)
        {
            _interactor = interactor;
        }

        public IDomainModel GetSemester(Guid semesterId)
        {
            var entity = Interactor.GetSemester(semesterId);

            return ConvertEntityToModel(entity);
        }

        protected SemesterDomainModel ConvertEntityToModel(IEntity<Guid> entity)
        {
            return new SemesterDomainModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public List<IDomainModel> GetAllSemesters()
        {
            var semesterModelList = new List<IDomainModel>();

            var semesters = Interactor.GetAllSemesters();

            foreach (var semesterEntity in semesters)
            {
                semesterModelList.Add(ConvertEntityToModel(semesterEntity));
            }

            return semesterModelList;
        }
    }
}