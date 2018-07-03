#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IBootstrapper.cs </Name>
//         <Created> 19/04/2018 6:48:20 PM </Created>
//         <Key> 809002a0-33c0-458f-b90e-5f73a7662f19 </Key>
//     </File>
//     <Summary>
//         IBootstrapper.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Contract.Repository.Interfaces
{
    public interface IBootstrapper
    {
        Task InitialAsync(CancellationToken cancellationToken = default);

        Task RebuildAsync(CancellationToken cancellationToken = default);
    }
}