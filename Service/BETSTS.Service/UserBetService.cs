using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models.User;
using Elect.DI.Attributes;
//using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BETSTS.Core.Utils;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(IUserBetService))]
    public class UserBetService : IUserBetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserBetEntity> _betRepository;
        private readonly IRepository<MatchEntity> _matchRepository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IRepository<FootballTeamEntity> _teamRepository;
        private readonly IRepository<ExchangeEntity> _exchangeRepository;
        private readonly IRepository<AmoutEntity> _amountRepository;

        public UserBetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _betRepository = unitOfWork.GetRepository<UserBetEntity>();
            _matchRepository = unitOfWork.GetRepository<MatchEntity>();
            _userRepository = unitOfWork.GetRepository<UserEntity>();
            _teamRepository = unitOfWork.GetRepository<FootballTeamEntity>();
            _exchangeRepository = unitOfWork.GetRepository<ExchangeEntity>();
            _amountRepository = unitOfWork.GetRepository<AmoutEntity>();
        }

        public Task<Guid> Create(UserBetModel model, Guid userId, CancellationToken cancellationToken = default)
        {
            CheckMatchExist(model.MatchId);
            CheckUserExist(userId);
            CheckBet(model.MatchId, userId);
            var match = _matchRepository.Get(x => x.Id == model.MatchId).Single();
            CheckTimeEditBet(match.TimeMatch);
            var betEntity = new UserBetEntity()
            {
                MatchId = model.MatchId,
                UserId = userId,
                TeamWinId = model.TeamWinId,
                TimeBet = SystemHelper.GetNetworkTime()
            };
            _betRepository.Add(betEntity);
            _unitOfWork.SaveChanges();
            return Task.FromResult(betEntity.Id);
        }

        public Task<UserBetEntity> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var betEntity = _betRepository.Get(x => x.Id == id).SingleOrDefault();
            return Task.FromResult(betEntity);
        }

        public Task Update(UpdateBetModel model, Guid userId, CancellationToken cancellationToken = default)
        {
            CheckBetExist(model.Id);
            var bet = _betRepository.Include(x=>x.Match).Single(x=>x.Id == model.Id);
            CheckUserBet(bet, userId);
            CheckTimeEditBet(bet.Match.TimeMatch);
            bet.TeamWinId = model.TeamWinId;
            _betRepository.Update(bet, x => x.TeamWinId);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<GetBetModel> GetAllBet(Guid userId)
        {
            var listBet = _betRepository.Get(x => x.UserId == userId).ToList();
            var listModel = new List<GetBetModel>();
            foreach (var bet in listBet)
            {
                var match = _matchRepository.Include(x => x.MatchTeams).Single(x => x.Id == bet.MatchId);
                if (bet.TeamWinId == Guid.Empty)
                {
                    var listTeamMatch = new List<TeamMatchModel>();
                    foreach (var team in match.MatchTeams)
                    {
                        var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                        var name = footballTeam?.Name;
                        listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                    }
                    var betModel = new GetBetModel()
                    {
                        MatchId = bet.MatchId,
                        MoneyLost = bet.MoneyLost,
                        SelectTeam = "",
                        SelectTeamId = bet.TeamWinId,
                        TeamMatches = listTeamMatch,
                        BetId = bet.Id
                    };
                    listModel.Add(betModel);
                }
                else
                {
                    var teamBet = _teamRepository.Get(x => x.Id == bet.TeamWinId).Single();
                    var listTeamMatch = new List<TeamMatchModel>();
                    foreach (var team in match.MatchTeams)
                    {
                        var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                        var name = footballTeam?.Name;
                        listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                    }
                    var betModel = new GetBetModel()
                    {
                        MatchId = bet.MatchId,
                        MoneyLost = bet.MoneyLost,
                        SelectTeam = teamBet.Name,
                        SelectTeamId = bet.TeamWinId,
                        TeamMatches = listTeamMatch,
                        BetId = bet.Id
                    };
                    listModel.Add(betModel);
                }
                
            }
            return listModel;
        }

        public IEnumerable<GetBetModel> GetAll(Guid userId)
        {
            var listMatch = _matchRepository.Include(x => x.MatchTeams, x => x.UserBets).Where(x => x.DeletedTime == null&& DateTime.Compare(x.TimeMatch,SystemHelper.GetNetworkTime()) > 0).OrderBy(x=>x.TimeMatch).ToList();
            var listModel = new List<GetBetModel>();
            foreach (var match in listMatch)
            {
                var matchModel = new GetBetModel();
                matchModel.TimeMatch = match.TimeMatch;
                matchModel.MatchId = match.Id;
                decimal total = 0;
                foreach (var bet in match.UserBets.Where(x => x.UserId == userId))
                {
                    if (bet.MatchId == match.Id)
                    {
                        matchModel.MoneyLost = bet.MoneyLost;
                        if (bet.TeamWinId == Guid.Empty)
                        {
                            matchModel.SelectTeam = "";
                            matchModel.SelectTeamId = bet.TeamWinId;
                            matchModel.BetId = bet.Id;
                        }
                        var footballTeam = _teamRepository.Get(x => x.Id == bet.TeamWinId).FirstOrDefault();
                        var name = footballTeam?.Name;
                        matchModel.SelectTeam = name;
                        matchModel.SelectTeamId = bet.TeamWinId;
                        matchModel.BetId = bet.Id;
                    }
                    else
                    {

                        matchModel.MoneyLost = 0;
                        matchModel.SelectTeam = null;
                        matchModel.SelectTeamId = null;
                        matchModel.BetId = null;
                    }
                    total += bet.MoneyLost;

                }
                var listTeamMatch = new List<TeamMatchModel>();
                foreach (var team in match.MatchTeams)
                {
                    var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                    var name = footballTeam?.Name;
                    listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                }
                matchModel.TeamMatches = listTeamMatch;
                listModel.Add(matchModel);
            }
            return listModel;
        }

        public IEnumerable<GetBetModel> GetHistory(Guid userId)
        {
            var listMatch = _matchRepository.Include(x => x.MatchTeams, x => x.UserBets).Where(x => x.DeletedTime == null && x.IsUpdated == 1 ).ToList();
            var listModel = new List<GetBetModel>();
            var user = _userRepository.Get(x => x.Id == userId).Single();
            foreach (var match in listMatch)
            {
                if (DateTime.Compare(match.TimeMatch, user.CreatedTime.DateTime) < 0)
                {
                    var matchModel = new GetBetModel();
                    matchModel.TimeMatch = match.TimeMatch;
                    matchModel.MatchId = match.Id;
                    matchModel.MoneyLost = 0;
                    matchModel.SelectTeam = null;
                    matchModel.SelectTeamId = null;
                    matchModel.BetId = null;
                    var listTeamMatch = new List<TeamMatchModel>();
                    foreach (var team in match.MatchTeams)
                    {
                        var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                        var name = footballTeam?.Name;
                        listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                    }
                    matchModel.TeamMatches = listTeamMatch;
                    listModel.Add(matchModel);
                }
                else
                {
                    var matchModel = new GetBetModel();
                    matchModel.TimeMatch = match.TimeMatch;
                    matchModel.MatchId = match.Id;
                    decimal total = 0;
                    foreach (var bet in match.UserBets.Where(x => x.UserId == userId))
                    {
                        if (bet.MatchId == match.Id)
                        {
                            matchModel.MoneyLost = bet.MoneyLost;
                            var footballTeam = _teamRepository.Get(x => x.Id == bet.TeamWinId).FirstOrDefault();
                            var name = footballTeam?.Name;
                            matchModel.SelectTeam = name;
                            matchModel.SelectTeamId = bet.TeamWinId;
                            matchModel.BetId = bet.Id;
                        }
                        else
                        {

                            matchModel.MoneyLost = 0;
                            matchModel.SelectTeam = null;
                            matchModel.SelectTeamId = null;
                            matchModel.BetId = null;
                        }
                        total += bet.MoneyLost;
                    }
                    var listTeamMatch = new List<TeamMatchModel>();
                    foreach (var team in match.MatchTeams)
                    {
                        var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                        var name = footballTeam?.Name;
                        listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                    }
                    matchModel.TeamMatches = listTeamMatch;
                    listModel.Add(matchModel);


                }
                //var listTeamMatch = new List<TeamMatchModel>();
                //foreach (var team in match.MatchTeams)
                //{
                //    var footballTeam = _teamRepository.Get(x => x.Id == team.TeamId).FirstOrDefault();
                //    var name = footballTeam?.Name;
                //    listTeamMatch.Add(new TeamMatchModel() { Name = name, Goal = team.Goals, Rate = team.Rate, TeamId = team.TeamId });
                //}
                //matchModel.TeamMatches = listTeamMatch;
                //listModel.Add(matchModel);
            }
            return listModel;
        }
        #region	Check

        public AmoutEntity GetHistoryExchange(Guid userId)
        {
            var user = _userRepository.Include(x => x.Amout.Exchanges).Single(x => x.Id == userId);
            var amount = _amountRepository.Include(x => x.Exchanges).Single(x => x.UserId == userId);
          //  var exchange = _exchangeRepository.Get(x => x.AmountId == amount.Id).ToList();
            return amount;
        }

        //public GetBetUser GetListBet(Guid matchId, GetBetUser model)
        //{
        //    var list = _betRepository.Get(x => x.MatchId == matchId);
        //    var match = _matchRepository.Include(x => x.MatchTeams).Single(x => x.Id == matchId);
        //    var listBet = new GetBetUser();
        //    listBet.FirstTeam = match.MatchTeams.First().FootballTeam.Name;
        //}
        private void CheckMatchExist(Guid id)
        {
            var exist = _matchRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "Match is not found");
            }
        }

        private void CheckUserExist(Guid id)
        {
            var exist = _userRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "User is not found");
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

        private void CheckBetExist(Guid id)
        {
            var exist = _betRepository.Get(x => x.Id == id).Any();
            if (!exist)
            {
                throw new CoreException(ErrorCode.NotFound, "The Bet is not found");
            }
        }

        private void CheckTimeEditBet(DateTime time)
        {
            if (DateTime.Compare(SystemHelper.GetNetworkTime(),time) > 0)
            {
                throw new CoreException(ErrorCode.Unknown, "Not edit bet when match start");
            }
        }

        private void CheckUserBet(UserBetEntity bet, Guid userId)
        {
            if (bet.UserId != userId)
            {
                throw new CoreException(ErrorCode.Unknown, "Không được chỉnh sửa bet của người khác nha mài");
            }
        }
        #endregion Check
    }
}