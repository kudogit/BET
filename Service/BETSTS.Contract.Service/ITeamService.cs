using BETSTS.Contract.Repository.Models;
using BETSTS.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BETSTS.Contract.Service
{
    public interface ITeamService
    {
        IEnumerable<FootballTeamEntity> GetAll();
        Task<Guid> Create(FootballTeamModel model);
        Task<FootballTeamEntity> Get(Guid id);
        Task Update(Guid id, FootballTeamModel model);
        Task Delete(Guid id);
    }
}