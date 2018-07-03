#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> TokenModel.cs </Name>
//         <Created> 22/04/2018 11:05:08 PM </Created>
//         <Key> f514cfc0-eec1-4464-bca0-ce0440c0fe86 </Key>
//     </File>
//     <Summary>
//         TokenModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace BETSTS.Core.Models.Authentication
{
    public class TokenModel
    {

        public string TokenType { get; set; } = "Bearer";

        public long ExpireIn { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

        public string State { get; set; }
    }
}