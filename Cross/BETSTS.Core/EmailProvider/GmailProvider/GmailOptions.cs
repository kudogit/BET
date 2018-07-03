#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> GmailOptions.cs </Name>
//         <Created> 22/04/2018 8:54:20 PM </Created>
//         <Key> f42e0ff6-e0c9-4665-a4b5-7d6230b46254 </Key>
//     </File>
//     <Summary>
//         GmailOptions.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace BETSTS.Core.EmailProvider.GmailProvider
{
    public class GmailOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }
    }
}