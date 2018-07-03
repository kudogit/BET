using BETSTS.Contract.Repository.Models;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BETSTS.Repository.Maps
{
    public class MatchTeamEntityMap : EntityTypeConfiguration<MatchTeamEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<MatchTeamEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(MatchTeamEntity));
        }
    }
}
