#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> ConfirmEmailModel.cs </Name>
//         <Created> 21/04/2018 5:58:13 PM </Created>
//         <Key> 96614e74-59ee-4505-a5b8-3e1ab8a47ba8 </Key>
//     </File>
//     <Summary>
//         ConfirmEmailModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace BETSTS.Core.Models.Authentication
{
    public class ConfirmEmailModel : SetPasswordModel
    {
        public string UserName { get; set; }
    }
}