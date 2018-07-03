using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models.Match;
using Elect.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Core.Models.User;
using BETSTS.Core.Utils;
using TeamMatchModel = BETSTS.Core.Models.Match.TeamMatchModel;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = (typeof(IMatchService)))]
    public class MatchService : IMatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MatchEntity> _matchRepository;
        private readonly IRepository<FootballTeamEntity> _teamRepository;
        private readonly IRepository<MatchTeamEntity> _matchTeamRepository;
        private readonly IRepository<UserBetEntity> _betRepository;
        private readonly IRepository<AmoutEntity> _amoutRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<ExchangeEntity> _exchangeRepository;
        public MatchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _matchRepository = unitOfWork.GetRepository<MatchEntity>();
            _teamRepository = unitOfWork.GetRepository<FootballTeamEntity>();
            _matchTeamRepository = unitOfWork.GetRepository<MatchTeamEntity>();
            _betRepository = unitOfWork.GetRepository<UserBetEntity>();
            _amoutRepository = unitOfWork.GetRepository<AmoutEntity>();
            _userRepository = unitOfWork.GetRepository<UserEntity>();
            _exchangeRepository = unitOfWork.GetRepository<ExchangeEntity>();
        }

        public async Task<Guid> Create(Guid userId, MatchModel model)
        {
            CheckTeamExist(model.FirstTeamId);

            CheckTeamExist(model.SecondTeamId);

            CheckIsAdmin(userId);

            var matchEntity = new MatchEntity()
            {
                TimeMatch = SystemHelper.UtcToSystemTime(model.TimeMatch),
                Stadium = model.Stadium,
                PriceConfigId = model.PriceConfigId,
                MatchTeams = new List<MatchTeamEntity>
                {
                    new MatchTeamEntity(){TeamId = model.FirstTeamId,Rate = model.FirstTeamRate},
                    new MatchTeamEntity(){TeamId = model.SecondTeamId,Rate = model.SecondTeamRate}
                }
            };

            _matchRepository.Add(matchEntity);
            await _unitOfWork.SaveChangesAsync();
            return matchEntity.Id;
        }

        public Task<MatchViewModel> Get(Guid id)
        {
            CheckExist(id);
            var match = _matchRepository.Include(x => x.MatchTeams).Single(x => x.Id == id);
            var matchModel = new MatchViewModel();
            matchModel.Stadium = match.Stadium;
            matchModel.TimeMatch = match.TimeMatch;
            matchModel.MatchId = match.Id;
            var listTeamMatch = new List<TeamMatchModel>();
            foreach (var team in match.MatchTeams)
            {
                var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                var name = footballTeam?.Name;
                listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
            }
            matchModel.TeamMatches = listTeamMatch;
            return Task.FromResult(matchModel);

        }

        public void UpdateScoreTeamMatch(Guid userId, Guid matchId, UpdateTeamMatch model)
        {
            CheckExist(matchId);
            CheckIsAdmin(userId);

            var match = _matchRepository.Include(x => x.MatchTeams, x => x.PriceConfig).Single(x => x.Id == matchId);

            CheckTimeUpdateScore(match.TimeMatch);

            var firstTeam = match.MatchTeams.First();
            var secondTeam = match.MatchTeams.Skip(1).First();

            firstTeam.Rate = model.FirstTeamRate;
            firstTeam.Goals = model.FirstTeamGoal;
            secondTeam.Goals = model.SecondTeamGoal;
            secondTeam.Rate = model.SecondTeamRate;
            _matchTeamRepository.Update(firstTeam, x => x.Goals, x => x.Rate);
            _matchTeamRepository.Update(secondTeam, x => x.Goals, x => x.Rate);

            var checkWin = Guid.NewGuid();

            var temp = Guid.NewGuid();

            var check = ((firstTeam.Goals + firstTeam.Rate) - (secondTeam.Goals + secondTeam.Rate));
            if (check > 0)
            {
                checkWin = firstTeam.TeamId;
            }
            else
            {
                if (check < 0)
                {
                    checkWin = secondTeam.TeamId;
                }
                else
                {
                    checkWin = temp;
                }
            }
            
            var listUser = _userRepository.Include(x => x.Amout, x => x.UserBets).Where(x => x.DeletedTime == null).ToList();

            foreach (var user in listUser)
            {
                var a = 0;
                if (DateTime.Compare(user.CreatedTime.DateTime, match.TimeMatch) > 0)
                {
                    a = 1;
                }

                if (a != 1)
                {
                    foreach (var userBet in user.UserBets)
                    {
                        if (userBet.MatchId == match.Id)
                        {
                            var amount = _amoutRepository.Get(x => x.UserId == userBet.UserId).Single();
                            userBet.MoneyLost = 0;
                            amount.Total -= userBet.MoneyLostLast;
                            if (userBet.TeamWinId == checkWin)
                            {
                                userBet.MoneyLost = 0;
                                userBet.MoneyLostLast = 0;
                                _betRepository.Update(userBet, x => x.MoneyLost, x => x.MoneyLostLast);

                            }
                            else
                            {
                                if (userBet.TeamWinId != Guid.Empty && checkWin == temp)
                                {
                                    userBet.MoneyLost = match.PriceConfig.Price * 50 / 100;
                                    userBet.MoneyLostLast = match.PriceConfig.Price * 50 / 100;
                                    _betRepository.Update(userBet, x => x.MoneyLost, x => x.MoneyLostLast);
                                }
                                else
                                {
                                    userBet.MoneyLost = match.PriceConfig.Price;
                                    userBet.MoneyLostLast = match.PriceConfig.Price;
                                    _betRepository.Update(userBet, x => x.MoneyLost, x => x.MoneyLostLast);
                                }

                            }
                            amount.Total += userBet.MoneyLost;
                            _amoutRepository.Update(amount, x => x.Total);
                            a = 1;
                            break;
                        }
                    }
                }

                if (a == 0)
                {
                    var betEntity = new UserBetEntity()
                    {
                        UserId = user.Id,
                        MatchId = match.Id,
                        MoneyLost = match.PriceConfig.Price,
                        MoneyLostLast = match.PriceConfig.Price,
                        IsUpdated = 1

                    };
                    var amount = _amoutRepository.Get(x => x.UserId == user.Id).Single();
                    amount.Total += match.PriceConfig.Price;
                    _amoutRepository.Update(amount, x => x.Total);
                    _betRepository.Add(betEntity);
                    _unitOfWork.SaveChanges();
                }
            }
            match.IsUpdated = 1;
            _matchRepository.Update(match, x => x.IsUpdated);
            _unitOfWork.SaveChanges();
        //    UpdateTotalAmoutAll(userId);
        }


        public IEnumerable<MatchViewModel> GetAll(string filter)
        {
            var matchList = _matchRepository.Include(x => x.MatchTeams).Where(x => x.DeletedTime == null).ToList();
            if (filter == "dontUpdate")
            {
                matchList = matchList.Where(x =>
                    x.IsUpdated == 0 && DateTime.Compare(SystemHelper.GetNetworkTime(), x.TimeMatch) > 0).ToList();
            }
            if (filter == "dontStart")
            {
                matchList = matchList.Where(x=> DateTime.Compare(SystemHelper.GetNetworkTime(), x.TimeMatch) < 0).ToList();
            }
            if (filter == "finished")
            {
                matchList = matchList.Where(x => x.IsUpdated == 1 && DateTime.Compare(SystemHelper.GetNetworkTime(), x.TimeMatch) > 0).ToList();
            }
            var listMatchModel = new List<MatchViewModel>();
            foreach (var item in matchList)
            {
                var matchModel = new MatchViewModel();
                matchModel.Stadium = item.Stadium;
                matchModel.TimeMatch = item.TimeMatch;
                matchModel.MatchId = item.Id;
                var listTeamMatch = new List<TeamMatchModel>();
                foreach (var team in item.MatchTeams)
                {
                    var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                    var name = footballTeam?.Name;
                    listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                }
                matchModel.TeamMatches = listTeamMatch;
                listMatchModel.Add(matchModel);
            }
            return listMatchModel;
        }
     
        public Task UpdateTotalAmoutAll(Guid userId)
        {
            CheckIsAdmin(userId);
            var listUser = _userRepository.Include(x => x.Amout, x => x.UserBets).Where(x => x.DeletedTime == null).ToList();
            var listMatch = _matchRepository.Include(x => x.PriceConfig).Where(x => x.DeletedTime == null && x.IsUpdated == 1).ToList();
            foreach (var match in listMatch)
            {

                foreach (var user in listUser)
                {
                    var a = 0;
                    if (DateTime.Compare(user.CreatedTime.DateTime, match.TimeMatch) > 0)
                    {
                        a = 1;
                    }
                    if (a != 1)
                    {
                        foreach (var userBet in user.UserBets)
                        {
                            if (userBet.MatchId == match.Id)
                            {
                                a = 1;
                                break;
                            }
                        }
                    }

                    if (a == 0)
                    {
                        var bet = new UserBetEntity()
                        {
                            UserId = user.Id,
                            MatchId = match.Id,
                            MoneyLost = match.PriceConfig.Price,
                            IsUpdated = 1

                        };
                        _betRepository.Add(bet);
                        _unitOfWork.SaveChanges();
                    }
                }
            }
            foreach (var user in listUser)
            {
                var amountEntity = _amoutRepository.Get(x => x.UserId == user.Id).Single();
                amountEntity.Total = 0;
                foreach (var bet in user.UserBets)
                {
                   
                    amountEntity.Total += bet.MoneyLost;
                    
                }
                _amoutRepository.Update(amountEntity, x => x.Total);
            }
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

        public Task SentMoney(Guid userId, Guid id, ExchargeModel model)
        {
            CheckIsAdmin(userId);
            CheckUser(id);

            var userAmount = _amoutRepository.Get(x => x.UserId == id).Single();
            var excharge = new ExchangeEntity()
            {
                Total = model.Total,
                Sent = model.Sent,
                Comment = model.Comment,
                AmountId = userAmount.Id
            };
            userAmount.Sent += model.Sent;
            userAmount.Total += model.Total;
            _amoutRepository.Update(userAmount, x => x.Sent,x=>x.Total);
            _exchangeRepository.Add(excharge);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }


        public Task Pay(Guid userId, Guid id, ExchargeModel model)
        {
            CheckIsAdmin(userId);
            CheckUser(id);
           
            var userAmount = _amoutRepository.Get(x => x.UserId == id).Single();
            var excharge = new ExchangeEntity()
            {
                Total = model.Total,
                Sent = model.Sent,
                Comment = model.Comment,
                AmountId = userAmount.Id
            };
            userAmount.Sent += model.Sent;
            userAmount.Total += model.Total;
            _amoutRepository.Update(userAmount, x => x.Sent);
            _exchangeRepository.Add(excharge);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
        public Task Delete(Guid userId, Guid id)
        {
            CheckIsAdmin(userId);
            CheckExist(id);
            var match = _matchRepository.Include(x => x.MatchTeams).Single(x => x.Id == id);
            foreach (var matchTeam in match.MatchTeams)
            {
                _matchTeamRepository.Delete(matchTeam);
            }
            _matchRepository.Delete(new MatchEntity { Id = id });
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
        public void CheckExist(Guid id)
        {
            var exist = _matchRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "The Match is not found.");
            }
        }
        public void CheckTeamExist(Guid id)
        {
            var exist = _teamRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "The Football Team is not found.");
            }
        }

        private void CheckUser(Guid id)
        {
            var exist = _userRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "User is not found");
            }
        }
        public void CheckTeamMatchExist(Guid id)
        {
            var exist = _matchTeamRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "The team match is not exist");
            }
        }

        private void CheckTimeUpdateScore(DateTime time)
        {
            if (DateTime.Compare(SystemHelper.GetNetworkTime(),time) < 0)
            {
                throw new CoreException(ErrorCode.Unknown, "Chưa đá mà chỉnh gì bác");
            }
        }
        private void CheckBet(Guid matchId, Guid userId)
        {
            var unique = _betRepository.Get(x => x.MatchId == matchId && x.UserId == userId).Any();
            if (unique)
            {
                throw new CoreException(ErrorCode.NotUnique, "Một thằng chỉ được bắt trận đấu 1 lần");
            }
        }
        public void CheckIsAdmin(Guid id)
        {
            var isAdmin = _userRepository.Get(x => x.Id == id).SingleOrDefault(x => x.IsAdmin == 1);
            if (isAdmin == null)
            {
                throw new CoreException(ErrorCode.UnAuthenticated, "Không phải việc của mài !!!");
            }
        }
    }
}