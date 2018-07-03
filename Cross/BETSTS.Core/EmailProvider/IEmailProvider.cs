#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IEmailProvider.cs </Name>
//         <Created> 22/04/2018 1:02:58 PM </Created>
//         <Key> f1b78315-fa0d-4b8e-96c4-ff8f9120d6be </Key>
//     </File>
//     <Summary>
//         IEmailProvider.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Core.EmailProvider
{
    public interface IEmailProvider
    {
        Task SendAsync(string email, string subject, string html, CancellationToken cancellationToken = default);
    }
}