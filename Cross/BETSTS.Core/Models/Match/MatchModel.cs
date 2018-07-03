using System;
using System.Collections.Generic;

namespace BETSTS.Core.Models.Match
{
    public class MatchModel
    {
        public DateTime TimeMatch { get; set; }
        public string Stadium { get; set; }
        public Guid PriceConfigId { get; set; }
        public decimal FirstTeamRate { get; set; }
        public Guid FirstTeamId { get; set; }
        public decimal SecondTeamRate { get; set; }
        public Guid SecondTeamId { get; set; }
    }

    public class MatchViewModel
    {
        public Guid MatchId { get; set; }
        public DateTime TimeMatch { get; set; }
        public string Stadium { get; set; }
        public IEnumerable<TeamMatchModel> TeamMatches { get; set; }
    }

    public class TeamMatchModel
    {
        public string Name { get; set; }
        public int Goal { get; set; }
        public decimal Rate { get; set; }
        public Guid TeamId { get; set; }
    }
    public class CreateTeamMatchModel
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public Guid TeamId { get; set; }
    }

    public class UpdateTeamMatch
    {
        public decimal FirstTeamRate { get; set; }
        public int FirstTeamGoal { get; set; }
        public decimal SecondTeamRate { get; set; }
        public int SecondTeamGoal { get; set; }
    }
    

}
