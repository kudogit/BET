#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 22/04/2018 3:17:33 PM </Created>
//         <Key> 80c0374c-a730-4bdc-b28f-f7a7b8f80a0e </Key>
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

namespace BETSTS.Core.EmailProvider.SendGridProvider
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSendGridEmailProvider(this IServiceCollection services, [NotNull]SendGridOptions options)
        {
            return services.AddSendGridEmailProvider(_ =>
            {
                _.DisplayEmail = options.DisplayEmail;
                _.DisplayName = options.DisplayName;
                _.Key = options.Key;
            });
        }

        public static IServiceCollection AddSendGridEmailProvider(this IServiceCollection services, [NotNull] Action<SendGridOptions> configuration)
        {
            services.Configure(configuration);

            services.AddScopedIfNotExist<IEmailProvider, SendGridEmailProvider>();

            return services;
        }
    }
}