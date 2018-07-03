using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Contract.Service.Base
{
    public interface IUpdateable<in T> where T : class, new()
    {
        Task UpdateAsync(T model, CancellationToken cancellationToken = default);
    }
}