using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Core.Models.User;

namespace BETSTS.Contract.Service
{
    public interface IUserBetService
    {
        Task<Guid> Create(UserBetModel model,Guid userId, CancellationToken cancellationToken = default);
        Task<UserBetEntity> Get(Guid id, CancellationToken cancellationToken = default);
        Task Update(UpdateBetModel model, Guid userId, CancellationToken cancellationToken = default);
        IEnumerable<GetBetModel> GetAllBet(Guid userId);
        IEnumerable<GetBetModel> GetAll(Guid userId);
        IEnumerable<GetBetModel> GetHistory(Guid userId);
        AmoutEntity GetHistoryExchange(Guid userId);

    }
}