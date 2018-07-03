using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;

namespace BETSTS.Contract.Repository.Models
{
    public class PriceConfigurationEntity : Entity<Guid>
    {
        public decimal Price { get; set; }
        public virtual ICollection<MatchEntity> Matches { get; set; }
    }
}
