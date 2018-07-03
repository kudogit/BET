using System;
using System.Collections.Generic;
using System.Text;
using Elect.Data.EF.Models;

namespace BETSTS.Contract.Repository.Models.User
{
    public class ExchangeEntity : Entity<Guid>
    {
        public decimal Sent { get; set; }
        public decimal Total { get; set; }
        public string Comment { get; set; }
        public Guid AmountId { get; set; }
        public virtual AmoutEntity Amout { get; set; }
    }
}
