#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UserEntityMap.cs </Name>
//         <Created> 27/03/2018 4:59:44 PM </Created>
//         <Key> 36a6873f-3968-4f81-83ac-957ee8ef60b2 </Key>
//     </File>
//     <Summary>
//         UserEntityMap.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using BETSTS.Contract.Repository.Models.User;
using Elect.Data.EF.Services.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BETSTS.Repository.Maps.User
{
    public class UserEntityMap : EntityTypeConfiguration<UserEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(UserEntity));

            builder.HasIndex(x => x.UserName);

            builder.HasIndex(x => x.Phone);

            builder.HasIndex(x => x.Email);

            builder.HasIndex(x => x.OTP);
            builder.HasIndex(x => x.UserName);
            builder.HasOne(x => x.Amout).WithOne(y => y.User).HasForeignKey<AmoutEntity>(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.UserBets).WithOne(y => y.User).HasForeignKey(y => y.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}