using System;
using Elect.Data.EF.Models;

namespace BETSTS.Contract.Repository.Models
{
    public class RuleConfigurationEntity : Entity<Guid>
    {
        public decimal Lost { get; set; }
        public decimal Draw { get; set; }
    }
}
