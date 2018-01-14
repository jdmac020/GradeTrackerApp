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
    public class MockRepository<TEntity,TKey> : Repository<TEntity,TKey> where TEntity : class, IEntity<TKey>
    {
        private List<TEntity> _dataSet;

        public MockRepository()
        {
            _dataSet = new List<TEntity>();
        }

        public override TEntity GetById(TKey id)
        {
            return _dataSet.Find(e => e.Id.Equals(id));
        }

        public override TEntity Create(TEntity entity)
        {
            if (entity.Id.Equals(Guid.Empty) || string.IsNullOrEmpty(entity.Name))
                throw new InvalidOperationException(); // is this what happens when you insert a bad entity??

            _dataSet.Add(entity);

            return _dataSet.LastOrDefault();
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
