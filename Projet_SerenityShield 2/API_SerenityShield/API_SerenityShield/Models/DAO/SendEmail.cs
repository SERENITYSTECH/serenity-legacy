using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace API_SerenityShield.Models.DAO
{
    public class SendEmail
    {
        #region email

        private static readonly HttpClient HttpClient = new HttpClient();
        private static IConfiguration Configuration { get; set; }
     
        public async void SendMailSimple(string mailAdress,string subject,string message)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true)
                 .AddJsonFile($"appsettings.{env}.json", optional: true)
                 .Build();

            // Retrieve the API key.new System.Collections.Generic.IDictionaryDebugView<string, string>(((Microsoft.Extensions.Configuration.ConfigurationProvider)(new System.Collections.Generic.ICollectionDebugView<Microsoft.Extensions.Configuration.IConfigurationProvider>(((Microsoft.Extensions.Configuration.ConfigurationRoot)configuration).Providers).Items[0])).Data).Items[4]
            var apiKey = Environment.GetEnvironmentVariable("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") ?? configuration["SendGrid:ApiKey"];

            var client = new SendGridClient(new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
            var from = new EmailAddress("hostmaster@serenityshield.io", "Serenity Shield");
            var sub = subject;
            var to = new EmailAddress(mailAdress);
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, sub, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var r = response.StatusCode;
        
        }

       
        public async void SendMailCheck(string email,string name,string firstname)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true)
                 .AddJsonFile($"appsettings.{env}.json", optional: true)
                 .Build();

            // Retrieve the API key.new System.Collections.Generic.IDictionaryDebugView<string, string>(((Microsoft.Extensions.Configuration.ConfigurationProvider)(new System.Collections.Generic.ICollectionDebugView<Microsoft.Extensions.Configuration.IConfigurationProvider>(((Microsoft.Extensions.Configuration.ConfigurationRoot)configuration).Providers).Items[0])).Data).Items[4]
            var apiKey = Environment.GetEnvironmentVariable("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") ?? configuration["SendGrid:ApiKey"];
           RandomCode rc=new RandomCode();
            string code = rc.CreateRandomCode(4);
            var client = new SendGridClient(new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
            var from = new EmailAddress("hostmaster@serenityshield.io", "Serenity Shield");
            var subject = "Verification";
            var to = new EmailAddress(email);
            var plainTextContent = "Hello "+name+" "+firstname+"<br>Your security code for email is :"+code;
            var htmlContent = "<strong>Hello " + name + " " + firstname + "<br>Your security code  for email is : <H1>" + code+"</H1></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var r = response.StatusCode;

        }
        public async void SendMailCheckCode(string email, string name, string firstname, string code)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true)
                 .AddJsonFile($"appsettings.{env}.json", optional: true)
                 .Build();

            // Retrieve the API key.new System.Collections.Generic.IDictionaryDebugView<string, string>(((Microsoft.Extensions.Configuration.ConfigurationProvider)(new System.Collections.Generic.ICollectionDebugView<Microsoft.Extensions.Configuration.IConfigurationProvider>(((Microsoft.Extensions.Configuration.ConfigurationRoot)configuration).Providers).Items[0])).Data).Items[4]
            var apiKey = Environment.GetEnvironmentVariable("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") ?? configuration["SendGrid:ApiKey"];

            var client = new SendGridClient(new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
            var from = new EmailAddress("hostmaster@serenityshield.io", "Serenity Shield");
            var subject = "Verification";
            var to = new EmailAddress(email);
            var plainTextContent = "Hello " + name + " " + firstname + "<br>Your security code for Email is :" + code;
            var htmlContent = "<strong>Hello " + name + " " + firstname + "<br>Your security code for sms is : <H1>" + code + "</H1></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var r = response.StatusCode;

        }
        public async void SendSmsCheckCode(string email, string name, string firstname, string code)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true)
                 .AddJsonFile($"appsettings.{env}.json", optional: true)
                 .Build();

            // Retrieve the API key.new System.Collections.Generic.IDictionaryDebugView<string, string>(((Microsoft.Extensions.Configuration.ConfigurationProvider)(new System.Collections.Generic.ICollectionDebugView<Microsoft.Extensions.Configuration.IConfigurationProvider>(((Microsoft.Extensions.Configuration.ConfigurationRoot)configuration).Providers).Items[0])).Data).Items[4]
            var apiKey = Environment.GetEnvironmentVariable("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") ?? configuration["SendGrid:ApiKey"];

            var client = new SendGridClient(new SendGridClientOptions { ApiKey = apiKey, HttpErrorAsException = true });
            var from = new EmailAddress("hostmaster@serenityshield.io", "Serenity Shield");
            var subject = "Verification";
            var to = new EmailAddress(email);
            var plainTextContent = "Hello " + name + " " + firstname + "<br>Your security code for sms is :" + code;
            var htmlContent = "<strong>Hello " + name + " " + firstname + "<br>Your security code for sms is : <H1>" + code + "</H1></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            var r = response.StatusCode;

        }
        #endregion email
        #region sms

        #endregion sms

    }
    public class SendEmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
    public class SendEmailCheckCodeModel
    {
        public string Email { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

    }
}
