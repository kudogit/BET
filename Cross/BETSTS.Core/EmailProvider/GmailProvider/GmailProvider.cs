#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> GmailProvider.cs </Name>
//         <Created> 22/04/2018 8:55:18 PM </Created>
//         <Key> 82c088db-f815-466b-9509-c20b76844838 </Key>
//     </File>
//     <Summary>
//         GmailProvider.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Core.EmailProvider.GmailProvider
{
    public class GmailProvider : IEmailProvider
    {
        private readonly GmailOptions _options;

        public GmailProvider(IOptions<GmailOptions> configuration)
        {
            _options = configuration.Value;
        }

        public async Task SendAsync(string email, string subject, string html, CancellationToken cancellationToken = default)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.DisplayName, _options.UserName));

            emailMessage.To.Add(new MailboxAddress(email, email));

            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = html
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTlsWhenAvailable, cancellationToken).ConfigureAwait(false);

                await client.AuthenticateAsync(_options.UserName, _options.Password, cancellationToken).ConfigureAwait(false);

                await client.SendAsync(emailMessage, cancellationToken).ConfigureAwait(true);
            }
        }
    }
}