#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserProfileEntityMap.cs </Name>
//         <Created> 16/04/2018 8:58:17 PM </Created>
//         <Key> 97b3566f-cc32-46c9-b99e-a1ff21703864 </Key>
//     </File>
//     <Summary>
//         UserProfileEntityMap.cs is a part of Elate
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
    public class UserProfileEntityMap : EntityTypeConfiguration<UserProfileEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserProfileEntity> builder)
        {
            base.Map(builder);

            builder.ToTable(nameof(UserProfileEntity));

            builder.HasOne(a => a.User).WithOne(b => b.Profile).HasForeignKey<UserProfileEntity>(e => e.Id);
        }
    }
}