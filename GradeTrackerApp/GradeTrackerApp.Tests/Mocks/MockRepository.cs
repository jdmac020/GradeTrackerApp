using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockRepository<TEntity> : Repository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
        private List<TEntity> _dataSet;

        public MockRepository()
        {
            _dataSet = new List<TEntity>();
        }

        public override TEntity GetById(Guid id)
        {
            return _dataSet.Find(e => e.Id.Equals(id));
        }

        public override Guid Create(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
                throw new InvalidOperationException(); // is this what happens when you insert a bad entity??

            entity.Id = Guid.NewGuid();

            _dataSet.Add(entity);

            return _dataSet.LastOrDefault().Id;
        }

        public override void Update(TEntity entity)
        {
            _dataSet.Remove(entity);
            _dataSet.Add(entity);
        }

        public override void Delete(TEntity entity)
        {
            _dataSet.Remove(entity);
        }

        public override IList<TEntity> GetAll()
        {
            return _dataSet;
        }
    }
}
