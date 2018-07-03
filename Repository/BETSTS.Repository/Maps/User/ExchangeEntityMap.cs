using System;
using System.Collections.Generic;
using System.Text;
using BETSTS.Contract.Repository.Models.User;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BETSTS.Repository.Maps.User
{
    public class ExchangeEntityMap : EntityTypeConfiguration<ExchangeEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<ExchangeEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(ExchangeEntity));

            builder.HasOne(x => x.Amout).WithMany(y => y.Exchanges).HasForeignKey(x => x.AmountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
