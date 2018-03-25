using System;
using System.Collections.Generic;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Interactors.Semester
{
    public class SemesterInteractor : ISemesterInteractor
    {

        public IRepository<SemesterEntity, Guid> Repo
        {
            get { return _repo ?? (_repo = new Repository<SemesterEntity, Guid>()); }
            set { _repo = value; }
        }

        private IRepository<SemesterEntity, Guid> _repo;

        public SemesterInteractor() { }

        public SemesterInteractor(IRepository<SemesterEntity, Guid> repo)
        {
            _repo = repo;
        }

        public SemesterEntity GetSemester(Guid semesterId)
        {
            if (semesterId.Equals(Guid.Empty))
                throw new ObjectNotFoundException("There is no Semester with that Id");

            return Repo.GetById(semesterId);
        }

        public List<SemesterEntity> GetAllSemesters()
        {
            return Repo.GetAll().ToList();
        }
    }
}