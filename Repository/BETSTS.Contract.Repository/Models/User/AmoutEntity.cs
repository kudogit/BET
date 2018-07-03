using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;

namespace BETSTS.Contract.Repository.Models.User
{
    public class AmoutEntity : Entity<Guid>
    {
        public decimal Total { get; set; }
        public decimal Sent { get; set; }
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ICollection<ExchangeEntity> Exchanges { get; set; }
    }
}
