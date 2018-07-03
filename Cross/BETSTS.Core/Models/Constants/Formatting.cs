#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> Formatting.cs </Name>
//         <Created> 18/04/2018 4:49:30 PM </Created>
//         <Key> f2c1e405-fcce-4ccf-9c35-60218b24109c </Key>
//     </File>
//     <Summary>
//         Formatting.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Utils;
using Newtonsoft.Json;
using System.Globalization;

namespace BETSTS.Core.Models.Constants
{
    public class Formatting
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Elect.Core.Constants.Formatting.JsonSerializerSettings.Formatting,
            NullValueHandling = Elect.Core.Constants.Formatting.JsonSerializerSettings.NullValueHandling,
            MissingMemberHandling = Elect.Core.Constants.Formatting.JsonSerializerSettings.MissingMemberHandling,
            DateFormatHandling = Elect.Core.Constants.Formatting.JsonSerializerSettings.DateFormatHandling,
            ReferenceLoopHandling = Elect.Core.Constants.Formatting.JsonSerializerSettings.ReferenceLoopHandling,
            ContractResolver = Elect.Core.Constants.Formatting.JsonSerializerSettings.ContractResolver,
            DateFormatString = SystemHelper.Setting.DateTimeFormat,
            Culture = CultureInfo.CurrentCulture,
        };
    }
}