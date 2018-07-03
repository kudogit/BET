using BETSTS.Contract.Repository.Models.User;
using Elect.Data.EF.Models;
using System;

namespace BETSTS.Contract.Repository.Models
{
    public class UserBetEntity : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamWinId { get; set; }
        public int IsUpdated { get; set; } 
        public decimal MoneyLost { get; set; }
        public decimal MoneyLostLast { get; set; }
        public DateTime TimeBet { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual MatchEntity Match { get; set; }
    }
}
