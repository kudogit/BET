#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> Entity.cs </Name>
//         <Created> 20/04/2018 10:21:32 AM </Created>
//         <Key> 8b71ba77-8d67-445d-91d8-be6e9dd98918 </Key>
//     </File>
//     <Summary>
//         Entity.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Utils;
using System;

namespace BETSTS.Contract.Repository.Models
{
    public abstract class Entity : Elect.Data.EF.Models.Entity<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

            CreatedTime = LastUpdatedTime = SystemHelper.SystemTimeNow;
        }
    }
}