using System;
using System.Collections.Generic;
using System.Text;
using BETSTS.Contract.Repository.Models;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BETSTS.Repository.Maps
{
    public class FootballEntityMap : EntityTypeConfiguration<FootballTeamEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<FootballTeamEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(FootballTeamEntity));
            builder.HasMany(x => x.MatchTeams).WithOne(y => y.FootballTeam).HasForeignKey(y => y.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
