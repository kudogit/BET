#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> SystemSettingModel.cs </Name>
//         <Created> 21/04/2018 5:32:14 PM </Created>
//         <Key> 6bfdfa36-1e93-4463-9120-81cd77730f24 </Key>
//     </File>
//     <Summary>
//         SystemSettingModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Reflection;

namespace BETSTS.Core.Models
{
    public class SystemSettingModel
    {
        public static SystemSettingModel Instance { get; set; }

        public string ApplicationName { get; set; } = Assembly.GetEntryAssembly().GetName().Name;

        public Guid EncryptKey { get; set; }

        public int AuthorizeCodeStorageSeconds { get; set; } = 600;

        public string Domain { get; set; }

        /// <summary>
        ///     Config use datetime with TimeZone. Default is "UTC", See more: https://msdn.microsoft.com/en-us/library/gg154758.aspx 
        /// </summary>
        public string TimeZone { get; set; } = "UTC";

        public string DateFormat { get; set; } = "dd/MM/yyyy";

        public string TimeFormat { get; set; } = "hh:mm:ss tt";

        public string DateTimeFormat { get; set; } = "dd/MM/yyyy hh:mm:ss tt";
    }
}