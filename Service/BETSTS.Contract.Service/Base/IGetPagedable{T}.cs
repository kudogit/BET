using System.Threading;
using System.Threading.Tasks;
using Elect.Web.Api.Models;

namespace BETSTS.Contract.Service.Base
{
    public interface IGetPagedable<T> where T : class, new()
    {
        Task<PagedResponseModel<T>> GetPagedAsync(BETSTS.Core.Models.PagedRequestModel model, CancellationToken cancellationToken = default);
    }
}