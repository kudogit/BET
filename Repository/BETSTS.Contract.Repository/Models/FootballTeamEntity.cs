using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;

namespace BETSTS.Contract.Repository.Models
{
    public class FootballTeamEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Coach { get; set; }
        public int Point { get; set; }
        public virtual ICollection<MatchTeamEntity> MatchTeams { get; set; }
    }
}
