using BETSTS.Contract.Repository.Models;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BETSTS.Repository.Maps
{
    public class MatchEntityMap : EntityTypeConfiguration<MatchEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<MatchEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(MatchEntity));
            builder.HasMany(x => x.MatchTeams).WithOne(y => y.Match).HasForeignKey(y => y.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.UserBets).WithOne(y => y.Match).HasForeignKey(y => y.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
