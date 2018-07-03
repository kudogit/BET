#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> Bootstrapper.cs </Name>
//         <Created> 19/04/2018 6:54:59 PM </Created>
//         <Key> d0138da8-6188-4f28-9560-a6d388181710 </Key>
//     </File>
//     <Summary>
//         Bootstrapper.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Threading;
using System.Threading.Tasks;
using BETSTS.Contract.Repository.Interfaces;
using Elect.Data.EF.Interfaces.DbContext;
using Elect.DI.Attributes;
using Microsoft.EntityFrameworkCore;

namespace BETSTS.Repository
{
    [ScopedDependency(ServiceType = typeof(IBootstrapper))]
    public class Bootstrapper : IBootstrapper
    {
        private readonly IDbContext _dbContext;

        public Bootstrapper(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task InitialAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task RebuildAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}