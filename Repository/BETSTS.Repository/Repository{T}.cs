using BETSTS.Contract.Repository.Interfaces;
using Elect.Data.EF.Models;
using Elect.DI.Attributes;

namespace BETSTS.Repository
{
    [ScopedDependency(ServiceType = typeof(IRepository<>))]
    public class Repository<T> : Elect.Data.EF.Services.Repository.BaseEntityRepository<T>, IRepository<T> where T : BaseEntity, new()
    {
        public Repository(Elect.Data.EF.Interfaces.DbContext.IDbContext dbContext) : base(dbContext)
        {
        }
    }
}