using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BETSTS.Contract.Repository.Models;
using BETSTS.Core.Models.Match;

namespace BETSTS.Contract.Service
{
    public interface IPriceConfigService
    {
        Task<Guid> Create(PriceConfigurationModel model);
        IEnumerable<PriceConfigurationEntity> GetAll();
    }
}