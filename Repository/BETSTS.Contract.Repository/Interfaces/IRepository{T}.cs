using Elect.Data.EF.Models;

namespace BETSTS.Contract.Repository.Interfaces
{
    public interface IRepository<T> : Elect.Data.EF.Interfaces.Repository.IBaseEntityRepository<T> where T : BaseEntity, new()
    {
    }
}