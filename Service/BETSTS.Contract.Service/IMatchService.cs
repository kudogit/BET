using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BETSTS.Contract.Repository.Models;
using BETSTS.Core.Models.Match;
using BETSTS.Core.Models.User;
using Elect.Web.Api.Models;

namespace BETSTS.Contract.Service
{
    public interface IMatchService
    {
        Task<Guid> Create(Guid userId, MatchModel model);
        Task<MatchViewModel> Get(Guid id);
       // Task<PagedResponseModel<MatchEntity>> GetPagedAsync(PagedRequestModel model, CancellationToken cancellationToken);
        IEnumerable<MatchViewModel> GetAll(string filter);
        void UpdateScoreTeamMatch(Guid userId, Guid matchId, UpdateTeamMatch model);
        Task Delete(Guid userId,Guid id);
        Task UpdateTotalAmoutAll(Guid userId);
        Task SentMoney(Guid userId, Guid id, ExchargeModel model);
    }
}