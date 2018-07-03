#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> CodeChallengeMethod.cs </Name>
//         <Created> 23/04/2018 10:12:08 AM </Created>
//         <Key> 842e281a-440d-4aa3-ac63-424a02bb3ea4 </Key>
//     </File>
//     <Summary>
//         CodeChallengeMethod.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BETSTS.Core.Models.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CodeChallengeMethod
    {
        /// <summary>
        ///     SHA-256 code challenge method. 
        /// </summary>
        S256,

        /// <summary>
        ///     Plain code challenge method. 
        /// </summary>
        Plain
    }
}