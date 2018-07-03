#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> RefreshTokenEntity.cs </Name>
//         <Created> 16/04/2018 6:07:37 PM </Created>
//         <Key> 8ebbe556-806c-4492-85e8-f1c188498d56 </Key>
//     </File>
//     <Summary>
//         RefreshTokenEntity.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Contract.Repository.Models.User;
using System;

namespace BETSTS.Contract.Repository.Models
{
    public class RefreshTokenEntity : Entity
    {
        public string RefreshToken { get; set; }

        public DateTimeOffset? ExpireOn { get; set; }

        public int TotalUsage { get; set; } = 1;

        // User

        public Guid UserId { get; set; }

        public virtual UserEntity User { get; set; }

        // Marker

        public string MarkerName { get; set; }

        public string MarkerVersion { get; set; }

        // OS

        public string OsName { get; set; }

        public string OsVersion { get; set; }

        // Engine

        public string EngineName { get; set; }

        public string EngineVersion { get; set; }

        // Browser

        public string BrowserName { get; set; }

        public string BrowserVersion { get; set; }

        // Location

        public string IpAddress { get; set; }

        public string PostalCode { get; set; }

        public string CityName { get; set; }

        public string CountryIsoCode { get; set; }

        public string ContinentCode { get; set; }

        public string TimeZone { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? AccuracyRadius { get; set; }

        // Others

        public string DeviceType { get; set; }

        public string UserAgent { get; set; }

        public string DeviceHash { get; set; }
    }
}