using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.UnitOfWork;

namespace GradeTrackerApp.EntityFramework.Repositories.EfRepository
{
    public class EfRepository<TEntity, TKey> : IEfRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public virtual IUnitOfWork UnitOfWork { get; set; }

        public virtual IEfUnitOfWork EfUnitOfWork { get; set; }

        protected DbSet<TEntity> _objectset;
        protected virtual DbSet<TEntity> ObjectSet { get { return _objectset ?? (_objectset = EfUnitOfWork.Context.Set<TEntity>()); } }

        public EfRepository()
        {

        }

        public EfRepository(IEfUnitOfWork unitOfWork)
        {
            EfUnitOfWork = unitOfWork;
        }

        public virtual TEntity GetById(TKey id)
        {
            return ObjectSet.Find(id);
        }

        public virtual TKey Create(TEntity entity)
        {
            var result = ObjectSet.Add(entity);
            EfUnitOfWork.Commit();
            return result.Id;
        }
        public virtual void Update(TEntity entity)
        {

            ObjectSet.AddOrUpdate(entity);
            EfUnitOfWork.Commit();
        }

        public virtual void Delete(TEntity entity)
        {
            ObjectSet.Remove(entity);
            EfUnitOfWork.Commit();
        }

        public virtual IList<TEntity> GetAll()
        {
            return ObjectSet.Select(o => o).ToList();
        }

        public virtual IQueryable<TEntity> GetAllQueryable()
        {
            return ObjectSet;
        }
    }
}