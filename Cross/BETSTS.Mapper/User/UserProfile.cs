#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserProfile.cs </Name>
//         <Created> 20/04/2018 10:37:42 PM </Created>
//         <Key> bad13b73-1a16-4764-96c6-cde147859034 </Key>
//     </File>
//     <Summary>
//         UserProfile.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using AutoMapper;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Models.User;
using Elect.Mapper.AutoMapper.IMappingExpressionUtils;

namespace BETSTS.Mapper.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // UserEntity

            CreateMap<UserEntity, UserBasicInfoModel>().IgnoreAllNonExisting()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Profile.FullName))
                .ForMember(d => d.Gender, o => o.MapFrom(s => s.Profile.Gender))
                .ForMember(d => d.Birthday, o => o.MapFrom(s => s.Profile.Birthday));

       

            // UserSignInTrackingModel

            CreateMap<UserBasicInfoModel, UserSignInTrackingModel>().IgnoreAllNonExisting();
            CreateMap<UserSignInTrackingModel, UserBasicInfoModel>().IgnoreAllNonExisting();

            // LoggedInUserModel

            CreateMap<UserBasicInfoModel, LoggedInUserModel>().IgnoreAllNonExisting();
        }
    }
}