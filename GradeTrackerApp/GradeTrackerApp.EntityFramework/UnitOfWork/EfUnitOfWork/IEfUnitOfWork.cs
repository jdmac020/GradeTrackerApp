using System.Data.Entity;

namespace GradeTrackerApp.EntityFramework.UnitOfWork.EfUnitOfWork
{
    public interface IEfUnitOfWork
    {
        DbContext  Context { get; set; }
    }
}
