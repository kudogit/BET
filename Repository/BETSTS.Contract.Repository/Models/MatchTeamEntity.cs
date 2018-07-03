using Elect.Data.EF.Models;
using System;

namespace BETSTS.Contract.Repository.Models
{
    public class MatchTeamEntity : Entity<Guid>
    {
        public int Goals { get; set; }
        public decimal Rate { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public virtual MatchEntity Match { get; set; }
        public virtual FootballTeamEntity FootballTeam { get; set; }
    }
}
