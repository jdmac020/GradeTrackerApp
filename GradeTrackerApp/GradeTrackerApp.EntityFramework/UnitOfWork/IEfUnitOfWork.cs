
using System.Data.Entity;

namespace GradeTrackerApp.EntityFramework.UnitOfWork
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; set; }
    }
}