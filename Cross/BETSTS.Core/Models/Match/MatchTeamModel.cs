using System;

namespace BETSTS.Core.Models.Match
{
    public class MatchTeamModel
    {
        public int Goals { get; set; }
        public int Rate { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
    }
}
