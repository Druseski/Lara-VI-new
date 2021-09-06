
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lara_VI.Repositories.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailJetSettings MailJetSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           MailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();
            return Execute(email,subject,htmlMessage);
        }

        public async Task Execute( string subject, string body, string email)
        {
            MailjetClient client = new MailjetClient(MailJetSettings.ApiKey, MailJetSettings.SecretKey);
          
            MailjetRequest request = new MailjetRequest
            {
                
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "lara.laravi2021@gmail.com"},
        {"Name", "Lara-Vi"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Dragan"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
       body
      }
     }
             });
            await client.PostAsync(request);
            
        }
    }

}
