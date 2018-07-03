#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> ErrorCode.cs </Name>
//         <Created> 18/04/2018 8:06:55 PM </Created>
//         <Key> 55455835-5066-4422-81d8-839eb07927ab </Key>
//     </File>
//     <Summary>
//         ErrorCode.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Microsoft.AspNetCore.Http;

namespace BETSTS.Core.Exceptions
{
    public enum ErrorCode
    {
        // Common

        [ErrorCodeAttribute("Common", "Bad Request", StatusCodes.Status400BadRequest)]
        BadRequest,

        [ErrorCodeAttribute("Common", "Un-Authenticate", StatusCodes.Status401Unauthorized)]
        UnAuthenticated,

        [ErrorCodeAttribute("Common", "Forbidden", StatusCodes.Status403Forbidden)]
        UnAuthorized,

        [ErrorCodeAttribute("Common", "Not Found, the resource does not exist.", StatusCodes.Status404NotFound)]
        NotFound,

        [ErrorCodeAttribute("Common", "Oops! Something went wrong, please try again later.", StatusCodes.Status500InternalServerError)]
        Unknown,

        [ErrorCodeAttribute("Common", "The resource is already, please try another.")]
        NotUnique,

        [ErrorCodeAttribute("Common", "The Token is already expired.")]
        TokenExpired,

        [ErrorCodeAttribute("Common", "The Token is invalid.")]
        TokenInvalid,

        // Client

        [ErrorCodeAttribute("Client", "Grant Type is invalid.")]
        GrantTypeInValid,

        [ErrorCodeAttribute("Client", "Secret is invalid.")]
        SecretInValid,

        // User

        [ErrorCodeAttribute("User", "Password is wrong.")]
        UserPasswordWrong,

        [ErrorCodeAttribute("User", "User is banned.")]
        UserBanned,

        [ErrorCodeAttribute("User", "User is not exist.")]
        UserNotFound,


        [ErrorCodeAttribute("User", "User dont have permission.")]
        DontHavePolicy,

        // Access Token
        [ErrorCodeAttribute("Access Token", "The Code is invalid.")]
        AuthCodeInValid
    }
}