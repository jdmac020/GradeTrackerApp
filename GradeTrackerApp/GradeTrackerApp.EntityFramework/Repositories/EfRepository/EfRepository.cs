using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.EntityFramework.Entities;
using GradeTrackerApp.EntityFramework.UnitOfWork;
using GradeTrackerApp.EntityFramework.UnitOfWork.EfUnitOfWork;

namespace GradeTrackerApp.EntityFramework.Repositories.EfRepository
{
    public class EfRepository<TEntity, TKey> : IEfRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public IEfUnitOfWork EfUnitOfWork { get; set; }

        public EfRepository() { }
        public EfRepository(IEfUnitOfWork unitOfWork) { EfUnitOfWork = unitOfWork; }

        private DbSet<TEntity> _objectset;
        protected DbSet<TEntity> ObjectSet { get { return _objectset ?? (_objectset = EfUnitOfWork.Context.Set<TEntity>()); } }

        public TEntity GetById(TKey id) { return ObjectSet.Find(id); }

        public TEntity Create(TEntity entity)
        {
            var result = ObjectSet.Add(entity);
            EfUnitOfWork.Commit();
            return result;
        }
        public void Update(TEntity entity)
        {

            ObjectSet.AddOrUpdate(entity);
            EfUnitOfWork.Commit();
        }

        public void Delete(TEntity entity)
        {
            ObjectSet.Remove(entity);
            EfUnitOfWork.Commit();
        }
        public IList<TEntity> GetAll() { return ObjectSet.Select(o => o).ToList(); }
    }
}
