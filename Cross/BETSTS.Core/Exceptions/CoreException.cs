#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> CoreException.cs </Name>
//         <Created> 18/04/2018 8:13:29 PM </Created>
//         <Key> 85cf22c1-0858-43f3-9b87-bfbf92cdedc4 </Key>
//     </File>
//     <Summary>
//         CoreException.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BETSTS.Core.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException(ErrorCode code, string message = "") : base(message)
        {
            Code = code;
        }

        public CoreException(ErrorCode code, Dictionary<string, object> additionalData) : this(code)
        {
            AdditionalData = additionalData;
        }

        public CoreException(ErrorCode code, string message, Dictionary<string, object> additionalData) : this(code, message)
        {
            AdditionalData = additionalData;
        }

        public ErrorCode Code { get; }

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}