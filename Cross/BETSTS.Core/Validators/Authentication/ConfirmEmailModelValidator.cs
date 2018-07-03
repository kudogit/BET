#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> ConfirmEmailModelValidator.cs </Name>
//         <Created> 21/04/2018 6:07:29 PM </Created>
//         <Key> 1d308e97-47b0-4e99-95f8-a89667f24dd0 </Key>
//     </File>
//     <Summary>
//         ConfirmEmailModelValidator.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Authentication;
using FluentValidation;

namespace BETSTS.Core.Validators.Authentication
{
    public class ConfirmEmailModelValidator : AbstractValidator<ConfirmEmailModel>
    {
        public ConfirmEmailModelValidator()
        {
            Include(new SetPasswordModelValidator());

            RuleFor(x => x.UserName).MaximumLength(200);
        }
    }
}