#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> AuthorizeModelValidator.cs </Name>
//         <Created> 14/05/2018 4:15:58 PM </Created>
//         <Key> 14240444-c566-46af-a4c6-65f1950b2704 </Key>
//     </File>
//     <Summary>
//         AuthorizeModelValidator.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Models.Constants;
using FluentValidation;

namespace BETSTS.Core.Validators.Authentication
{
    public class AuthorizeModelValidator : AbstractValidator<AuthorizeModel>
    {
        public AuthorizeModelValidator()
        {
            RuleFor(x => x.Continue).NotEmpty().When(x => x.GrantType == GrantType.AuthorizationCodePKCE || x.GrantType == GrantType.AuthorizationCode || x.GrantType == GrantType.Implicit);

            RuleFor(x => x.CodeChallenge).NotEmpty().When(x => x.GrantType == GrantType.AuthorizationCodePKCE);

            RuleFor(x => x.UserName).NotEmpty().MaximumLength(200);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 100)
                .WithMessage("Password must between 6 and 100 characters");
        }
    }
}