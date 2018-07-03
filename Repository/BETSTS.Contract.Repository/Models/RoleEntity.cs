using System;
using System.Collections.Generic;
using System.Text;
using BETSTS.Contract.Repository.Models.User;
using Elect.Data.EF.Models;

namespace BETSTS.Contract.Repository.Models
{
    public class RoleEntity : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
