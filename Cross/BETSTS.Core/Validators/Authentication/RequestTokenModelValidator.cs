#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> RequestTokenModelValidator.cs </Name>
//         <Created> 14/05/2018 4:18:57 PM </Created>
//         <Key> e7f14b94-887b-4132-936c-dd359aa1bb70 </Key>
//     </File>
//     <Summary>
//         RequestTokenModelValidator.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Models.Constants;
using FluentValidation;

namespace BETSTS.Core.Validators.Authentication
{
    public class RequestTokenModelValidator : AbstractValidator<RequestTokenModel>
    {
        public RequestTokenModelValidator()
        {
            // Code and Client Credential

            //RuleFor(x => x.ClientSecret)
            //    .NotEmpty()
            //    .When(x => x.GrantType == GrantType.AuthorizationCode
            //               || x.GrantType == GrantType.ClientCredential);
            
            //RuleFor(x => x.CodeVerifier)
            //    .NotEmpty()
            //    .When(x => x.GrantType == GrantType.AuthorizationCodePKCE);

            //RuleFor(x => x.RedirectUri)
            //    .NotEmpty()
            //    .When(x => x.GrantType == GrantType.AuthorizationCode
            //               || x.GrantType == GrantType.AuthorizationCodePKCE);

            // Resource Onwer Password
            
            RuleFor(x => x.UserName)
                .NotEmpty()
                .When(x => x.GrantType == GrantType.ResourceOwner);
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .When(x => x.GrantType == GrantType.ResourceOwner);

            // Refresh Token
            RuleFor(x => x.RefreshToken).NotEmpty().When(x => x.GrantType == GrantType.RefreshToken);
        }
    }
}