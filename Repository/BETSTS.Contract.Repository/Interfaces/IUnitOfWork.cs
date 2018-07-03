using Elect.Data.EF.Models;

namespace BETSTS.Contract.Repository.Interfaces
{
    public interface IUnitOfWork : Elect.Data.EF.Interfaces.UnitOfWork.IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, new();
    }
}