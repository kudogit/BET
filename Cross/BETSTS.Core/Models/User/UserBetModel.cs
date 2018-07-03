using System;
using System.Collections.Generic;

namespace BETSTS.Core.Models.User
{
    public class UserBetModel
    {
        public Guid MatchId { get; set; }
        public Guid TeamWinId { get; set; }
    }

    public class CreateBetModel : UserBetModel
    {

    }

    public class UpdateBetModel
    {
        public Guid Id { get; set; }
        public Guid TeamWinId { get; set; }
    }

    public class GetBetModel
    {
        public Guid MatchId { get; set; }
        public Guid? BetId { get; set; }
        public DateTime TimeMatch { get; set; }
        public string SelectTeam { get; set; }
        public Guid? SelectTeamId { get; set; }
        public decimal MoneyLost { get; set; }
        public IEnumerable<TeamMatchModel> TeamMatches { get; set; }
    }

    public class GetBetUser
    {
        public string FirstTeam { get; set; }
        public string SeCondTeam { get; set; }
        public IEnumerable<GetSelectTeam> SelectTeams { get; set; }


    }

    public class GetSelectTeam
    {
        public string UserName { get; set; }
        public string SelectTeamName { get; set; }
    }
    public class TeamMatchModel
    {
        public string Name { get; set; }
        public int Goal { get; set; }
        public decimal Rate { get; set; }
        public Guid TeamId { get; set; }
    }

    public class ExchargeModel
    {
        public decimal Total { get; set; }
        public decimal Sent { get; set; }
        public string Comment { get; set; }
    }

}
