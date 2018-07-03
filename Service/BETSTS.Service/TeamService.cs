using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models;
using Elect.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(ITeamService))]
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FootballTeamEntity> _teamRepository;

        public TeamService( IUnitOfWork unitOfWork)
        {
            _teamRepository = unitOfWork.GetRepository<FootballTeamEntity>();
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Create(FootballTeamModel model)
        {
            var entity = new FootballTeamEntity()
            {
                Name = model.Name,
                Country = model.Country,
                Coach = model.Coach
            };
            _teamRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public IEnumerable<FootballTeamEntity> GetAll()
        {
            return _teamRepository.Get();
        }

        public Task<FootballTeamEntity> Get(Guid id)
        {
            var result = _teamRepository.Get(x => x.Id == id).Single();
            return Task.FromResult(result);
        }

        public Task Update(Guid id, FootballTeamModel model)
        {
            CheckExist(id);
            var teamEntity = _teamRepository.Get(x => x.Id == id).Single();
            teamEntity.Coach = model.Coach;
            teamEntity.Country = model.Country;
            teamEntity.Name = model.Name;
            _teamRepository.Update(teamEntity,x=>x.Coach,x=>x.Country,x=>x.Name);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            CheckExist(id);
            _teamRepository.Delete(new FootballTeamEntity{Id = id});
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

        #region Check

        private void CheckExist(Guid id)
        {
            var exist = _teamRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound,"The Football team is not found");
            }
        }
        #endregion
    }
}
