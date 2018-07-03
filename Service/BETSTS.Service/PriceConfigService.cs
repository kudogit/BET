using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Service;
using BETSTS.Core.Models.Match;
using Elect.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(IPriceConfigService))]
    public class PriceConfigService : IPriceConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PriceConfigurationEntity> _priceConfigRepository;

        public PriceConfigService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _priceConfigRepository = unitOfWork.GetRepository<PriceConfigurationEntity>();
        }

        public async Task<Guid> Create(PriceConfigurationModel model)
        {
            var configEntity = new PriceConfigurationEntity()
            {
                Price = model.Price
            };
            _priceConfigRepository.Add(configEntity);
            await _unitOfWork.SaveChangesAsync();
            return configEntity.Id;
        }

        public IEnumerable<PriceConfigurationEntity> GetAll()
        {
            return _priceConfigRepository.Get().ToList();
        }
    }
}
