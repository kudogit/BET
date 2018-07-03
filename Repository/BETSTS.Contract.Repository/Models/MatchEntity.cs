using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;

namespace BETSTS.Contract.Repository.Models
{
    public class MatchEntity : Entity<Guid>
    {
        public DateTime TimeMatch { get; set; }
        public string Stadium { get; set; }
        public Guid PriceConfigId { get; set; }
        public int IsUpdated { get; set; }
        public virtual ICollection<MatchTeamEntity> MatchTeams { get; set; }
        public virtual ICollection<UserBetEntity> UserBets { get; set; }
        public virtual PriceConfigurationEntity PriceConfig { get; set; }
    }
}
