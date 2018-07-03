using BETSTS.Contract.Repository.Models.User;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BETSTS.Repository.Maps.User
{
    public class AmoutEntityMap : EntityTypeConfiguration<AmoutEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<AmoutEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(AmoutEntity));
        }
    }
}
