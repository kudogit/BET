#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> ErrorModel.cs </Name>
//         <Created> 18/04/2018 7:52:43 PM </Created>
//         <Key> 8d322cca-34af-4f41-b2da-6f40669d9026 </Key>
//     </File>
//     <Summary>
//         ErrorModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using EnumsNET;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace BETSTS.Core.Exceptions
{
    public class ErrorModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorCode Code { get; set; } = ErrorCode.Unknown;

        public string Message { get; set; }

        public string Module { get; set; }

        public int HttpResponseCode { get; set; } = StatusCodes.Status400BadRequest;

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();

        public ErrorModel()
        {
        }

        public ErrorModel(ErrorCode code, string message)
        {
            var errorCodeAttributeData =
                Enums
                    .GetMember<ErrorCode>(code.ToString())
                    .Value
                    .GetAttributes()
                    .Get<ErrorCodeAttribute>();

            Code = code;

            Message = string.IsNullOrWhiteSpace(message) ? errorCodeAttributeData.DefaultMessage : message;

            Module = errorCodeAttributeData.Group;

            HttpResponseCode = errorCodeAttributeData.HttpResponseCode;
        }

        public ErrorModel(ErrorCode code, string message, Dictionary<string, object> additionalData) : this(code,
            message)
        {
            AdditionalData = additionalData;
        }
    }
}