using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Contract.Service.Base
{
    public interface IGetable<T, in TKey> where T : class, new()
    {
        Task<T> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}