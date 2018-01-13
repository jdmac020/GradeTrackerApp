using System.Data.Entity;
using GradeTrackerApp.EntityFramework.Repositories.EfRepository;

namespace GradeTrackerApp.EntityFramework.UnitOfWork.EfUnitOfWork
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        DbContext  Context { get; set; }
    }
}
