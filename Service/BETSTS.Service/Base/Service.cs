#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> Service.cs </Name>
//         <Created> 20/04/2018 10:59:38 AM </Created>
//         <Key> 17fcd85b-c501-4b04-a681-8cdd1e12d49a </Key>
//     </File>
//     <Summary>
//         Service.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Contract.Repository.Interfaces;

namespace BETSTS.Service.Base
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}