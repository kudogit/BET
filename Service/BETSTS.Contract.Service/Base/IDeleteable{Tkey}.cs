using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Contract.Service.Base
{
    public interface IDeleteable<in TKey>
    {
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
    }
}