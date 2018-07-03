using System.Threading;
using System.Threading.Tasks;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;

namespace BETSTS.Contract.Service.Base
{
    public interface IGetDataTableable<T> where T : class, new()
    {
        Task<DataTableResponseModel<T>> GetDataTableAsync(DataTableRequestModel model, CancellationToken cancellationToken = default);
    }
}