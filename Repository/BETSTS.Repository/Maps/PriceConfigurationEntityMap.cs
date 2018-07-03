using System;
using System.Collections.Generic;
using System.Text;
using BETSTS.Contract.Repository.Models;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BETSTS.Repository.Maps
{
    public class PriceConfigurationEntityMap : EntityTypeConfiguration<PriceConfigurationEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<PriceConfigurationEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(PriceConfigurationEntity));
            builder.HasMany(x => x.Matches).WithOne(y => y.PriceConfig).HasForeignKey(y => y.PriceConfigId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
