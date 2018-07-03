#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> Endpoint.cs </Name>
//         <Created> 21/04/2018 10:15:45 PM </Created>
//         <Key> c89acce9-36da-48f1-9260-303a31f98276 </Key>
//     </File>
//     <Summary>
//         Endpoint.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using BETSTS.Core.Utils;
using Flurl;

namespace BETSTS.Core.Models.Constants
{
    public class Endpoint
    {
        public const string EmailTemplateEndpoint = "~/templates/email";

        public const string ConfirmEmailEndpoint = "~/portal/confirm-email";

        public const string ChangePasswordEndpoint = "~/portal/change-password";

        public static string GetAbsoluteEndpoint(string endpoint)
        {
            endpoint = endpoint.Trim(' ', '~');

            return SystemHelper.Setting.Domain.AppendPathSegment(endpoint);
        }
    }
}