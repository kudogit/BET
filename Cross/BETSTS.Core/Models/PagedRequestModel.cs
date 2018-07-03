#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> PagedRequestModel.cs </Name>
//         <Created> 20/04/2018 3:45:00 PM </Created>
//         <Key> 756020db-77d7-4264-8772-f32a61f97cc9 </Key>
//     </File>
//     <Summary>
//         PagedRequestModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace BETSTS.Core.Models
{
    public class PagedRequestModel : Elect.Web.Api.Models.PagedRequestModel
    {
        public PagedRequestModel()
        {
            Skip = 0;

            Take = 10;
        }
    }
}