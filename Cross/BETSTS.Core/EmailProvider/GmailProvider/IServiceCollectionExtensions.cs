#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 22/04/2018 8:54:56 PM </Created>
//         <Key> 079a71f8-7d9f-4a96-a6e9-1f11968b642c </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.DI;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BETSTS.Core.EmailProvider.GmailProvider
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGmailProvider(this IServiceCollection services, [NotNull]GmailOptions options)
        {
            return services.AddGmailProvider(_ =>
            {
                _.DisplayName = options.DisplayName;
                _.UserName = options.UserName;
                _.Password = options.Password;
            });
        }

        public static IServiceCollection AddGmailProvider(this IServiceCollection services, [NotNull] Action<GmailOptions> configuration)
        {
            services.Configure(configuration);

            services.AddScopedIfNotExist<IEmailProvider, GmailProvider>();

            return services;
        }
    }
}