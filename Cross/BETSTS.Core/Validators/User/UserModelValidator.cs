#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserModelValidator.cs </Name>
//         <Created> 20/04/2018 6:43:31 PM </Created>
//         <Key> e9138277-8823-4808-8eb4-5b93ece461da </Key>
//     </File>
//     <Summary>
//         UserModelValidator.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.User;
using FluentValidation;

namespace BETSTS.Core.Validators.User
{
    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Password))
                .MaximumLength(500);

            RuleFor(x => x.Password)
                .NotEmpty()
                .When(x => string.IsNullOrWhiteSpace(x.Username))
                .MaximumLength(20);
        }
    }

    public class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            Include(new CreateUserModelValidator());

            RuleFor(x => x.UserName)
                .MaximumLength(200);

            RuleFor(x => x.BannedRemark)
                .NotEmpty()
                .When(x => x.IsBanned)
                .MaximumLength(500);
        }
    }
}