#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> EmailModel.cs </Name>
//         <Created> 21/04/2018 10:25:53 PM </Created>
//         <Key> 81e1bf91-27fb-4640-ba02-37a922eabb48 </Key>
//     </File>
//     <Summary>
//         EmailModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BETSTS.Core.Models.Email
{
    public class EmailModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public EmailTemplate Template { get; set; }

        public string ApplicationName { get; set; }

        public string Url { get; set; }

        public DateTimeOffset? ExpireTime { get; set; }
    }
}