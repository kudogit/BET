#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> ApplicationType.cs </Name>
//         <Created> 16/04/2018 4:58:18 PM </Created>
//         <Key> 9ebab0df-f2aa-482b-be1e-afc9bbd0a5e5 </Key>
//     </File>
//     <Summary>
//         ApplicationType.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BETSTS.Core.Models.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApplicationType
    {
        /// <summary>
        ///     Mobile or Desktop, apps that run native in a device. eg: Android, iOS, Desktop
        ///     Universal App.
        /// </summary>
        Native,

        /// <summary>
        ///     A javascript frontend app that uses an API. eg: AngularJS, ReactJS. 
        /// </summary>
        [Display(Name = "Single Page Application")]
        SinglePageWebApp,

        /// <summary>
        ///     Traditional web app (with refresh). eg: PHP, Java, ASP .NET, .Net Core. 
        /// </summary>
        [Display(Name = "Regular Web Application")]
        RegularWebApp,

        /// <summary>
        ///     CLI, Daemons or Services running on your backend, background job. Not have UI and
        ///     interactive with end-user by any way.
        /// </summary>
        [Display(Name = "Machine to Machine Application")]
        NonInteractiveClient
    }
}