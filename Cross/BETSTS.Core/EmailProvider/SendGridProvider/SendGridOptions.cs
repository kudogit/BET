#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> SendGridOptions.cs </Name>
//         <Created> 22/04/2018 2:40:09 PM </Created>
//         <Key> 20668fc1-481c-4d46-a50f-7473c9b24fad </Key>
//     </File>
//     <Summary>
//         SendGridOptions.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace BETSTS.Core.EmailProvider.SendGridProvider
{
    public class SendGridOptions
    {
        public string Key { get; set; }

        public string DisplayEmail { get; set; }

        public string DisplayName { get; set; }
    }
}