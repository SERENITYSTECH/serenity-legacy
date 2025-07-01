using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;

namespace ServiceInactivityPeriod.AppCode
{
    public class SendEmail
    {
        #region email


        //for framework 4.7.2 
        public void SendMailSimple(string mailAdress, string subject, string message)
        {


            using (MailMessage mailMsg = new MailMessage())
            {
                // API key
                string apiKey = Environment.GetEnvironmentVariable("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");

                // To
                mailMsg.To.Add(new MailAddress(mailAdress));

                // From
                mailMsg.From = new MailAddress("hostmaster@serenityshield.io", "Serenity Shield");
                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                string text = message;
                string html = message;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                using (SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("apikey", "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");
                    smtpClient.Send(mailMsg);
                }
            }

        }
        public void SendMailInactivity(string mailAdress, string iduser,string idStrongBoxForHeir)
        {
            Cryptage cr = new Cryptage();

            string idUser = cr.EncryptHexa(iduser);
            string idSbh = cr.EncryptHexa(idStrongBoxForHeir);

            using (MailMessage mailMsg = new MailMessage())
            {
                // API key
                string apiKey = Environment.GetEnvironmentVariable("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");

                // To
                mailMsg.To.Add(new MailAddress(mailAdress));

                // From
                mailMsg.From = new MailAddress("hostmaster@serenityshield.io", "Serenity Shield");
                // Subject and multipart/alternative Body
                mailMsg.Subject = "Inactivity Period";
                string text = " Dear user, you are receiving this message as a result of your activity police activation settings."
                              + "<br> Thank you for validating the following green status by clicking the link below."
                              + "<br> We wish you a pleasant day."
                              + "<br> The Serenity Shield team.";
                string html = " Dear user, you are receiving this message as a result of your activity police activation settings."
                              + "<br> Thank you for validating the following green status by clicking the link below."
                              + "<br> We wish you a pleasant day."
                              + "<br> The Serenity Shield team."
                              + "<br><a target=\"_self\" href=\"https://api-test.serenityshield-works.io/InsertHistory/" + idUser + "_"+ idSbh +"\" >Click Here to confirm</a>";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                using (SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("apikey", "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");
                    smtpClient.Send(mailMsg);
                }
            }

        }

        public void SendMailToServiceInactivity(string iduser, IStrongboxForHeirs item)
        {
            Cryptage cr = new Cryptage();
            string now = DateTime.Now.ToString("yyyy/MM/dd");
            User user = new User();
            user = user.GetUserById(iduser);

            string message = "<H2>Customer inactivity check</H2><br><H2> Start of the process on :" + now + "</H2><br><br>"
                             + " Customer<br>"
                             + " ID:" + cr.EncryptHexa(user.Id) + "<br>"
                             + " Firstname:" + user.FirstName + "<br>"
                             + " Lastname:" + user.LastName + "<br>"
                             + " Phone Number:" + user.PhoneNumber + "<br>"
                             + " Email:" + user.Email + "<br><br>"
                             + " StrongBox For Heir<br>"
                             + " ID:" + cr.EncryptHexa(item.id) + "<br>"
                             + " Label:" + item.label + "<br>"
                             + " Message For Heir:" + item.messageForHeirs + "<br>"
                             + " Wallet public Key:" + item.walletPublicKeyOwner + "<br>"
                             + " Secret public Key:" + item.secretPK + "<br>"
                             + " Solana public Key:" + item.solanaPK + "<br>"
                             + " Paying public Key:" + item.payingPK + "<br>"
                             + " Smart Contract public Key:" + item.scPK + "<br>"
                             + " Code ID:" + item.codeID + "<br>"
                             + " NFT shard Serenity public Key:" + item.solSereNftPK + "<br>"
                             + " NFT shard User public Key:" + item.solUsrNftPK + "<br>"
                             + " NFT shard Heir public Key::" + item.solHeirNftPKs + "<br><br>";

            string content = "Content<br>";
            if (item.content != null)
            {


                if (item.content.walletDex.Count > 0)
                {
                    content += "Wallet Dex<br>";
                    foreach (var c in item.content.walletDex)
                    {
                        if (c.type != null)
                        {

                            content += "Label:" + c.label + "<br>"
                                     + "Provider:" + c.provider + "<br>"
                                     + "Seed:" + c.seed + "<br>"
                                     + "Type Wallet<br>"
                                     + "Label:" + c.type.label + "<br>"
                                     + "type:" + c.type.type + "<br>"
                                     + "Supported:" + c.type.supported + "<br>";
                        }
                        else
                        {
                            content += "Label:" + c.label + "<br>"
                              + "Provider:" + c.provider + "<br>";

                        }

                    }
                }
                if (item.content.walletCex.Count > 0)
                {
                    content += "Wallet Cex<br>";
                    foreach (var c in item.content.walletCex)
                    {
                        if (c.type != null)
                        {

                            content += "Label:" + c.label + "<br>"
                                     + "Provider:" + c.provider + "<br>"
                                     + "Type Wallet<br>"
                                     + "Label:" + c.type.label + "<br>"
                                     + "type:" + c.type.type + "<br>"
                                     + "Supported:" + c.type.supported + "<br>";
                        }
                        else
                        {
                            content += "Label:" + c.label + "<br>"
                              + "Provider:" + c.provider + "<br>";


                        }

                    }
                }
                if (item.content.walletDesktop.Count > 0)
                {
                    content += "Wallet Desktop<br>";
                    foreach (var c in item.content.walletDesktop)
                    {
                        if (c.type != null)
                        {

                            content += "Label:" + c.label + "<br>"
                                     + "Provider:" + c.provider + "<br>"
                                     + "Type Wallet<br>"
                                     + "Label:" + c.type.label + "<br>"
                                     + "type:" + c.type.type + "<br>"
                                     + "Supported:" + c.type.supported + "<br>";
                        }
                        else
                        {
                            content += "Label:" + c.label + "<br>"
                              + "Provider:" + c.provider + "<br>";


                        }

                    }
                }
                if (item.content.walletHardware.Count > 0)
                {
                    content += "Wallet Harware<br>";
                    foreach (var c in item.content.walletHardware)
                    {
                        if (c.type != null)
                        {

                            content += "Label:" + c.label + "<br>"
                                     + "Provider:" + c.provider + "<br>"
                                     + "Type Wallet<br>"
                                     + "Label:" + c.type.label + "<br>"
                                     + "type:" + c.type.type + "<br>"
                                     + "Supported:" + c.type.supported + "<br>";
                        }
                        else
                        {
                            content += "Label:" + c.label + "<br>"
                              + "Provider:" + c.provider + "<br>";


                        }

                    }
                }
            }
            content += "<br>";

            string listHeir = " Heirs<br>";
            if (item.heirs != null)
            {


                foreach (IHeir h in item.heirs)
                {
                    listHeir += " Firstname:" + h.firstName + "<br>"
                                 + " Lastname:" + h.lastName + "<br>"
                                 + " Phone Number:" + h.phone + "<br>"
                                 + " Email:" + h.email + "<br>"
                                 + " Public Key:" + h.publicKey + "<br>"
                                 + " Wallet Type:" + h.walletType.label + "<br>"
                                 + " Creation date:" + h.creation_Date + "<br><br>";
                }

            }

            using (MailMessage mailMsg = new MailMessage())
            {
                // API key
                string apiKey = Environment.GetEnvironmentVariable("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");

                // To
                mailMsg.To.Add(new MailAddress("policy-activity@serenityshield.io"));
                // mailMsg.To.Add(new MailAddress("Policy-activity@serenityshield.io"));

                // From
                mailMsg.From = new MailAddress("hostmaster@serenityshield.io", "Serenity Shield");
                // Subject and multipart/alternative Body
                mailMsg.Subject = "Period of inactivity exceeded";
                string text = message+content+listHeir;
                string html = message+content+listHeir;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                using (SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("apikey", "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");
                    smtpClient.Send(mailMsg);
                }
            }

        }
        public void SendMailToServiceInactivityToUnlockStrongBoxForHeir(string iduser, IStrongboxForHeirs item)
        {
            Cryptage cr = new Cryptage();
            string now = DateTime.Now.ToString("yyyy/MM/dd");
            User user = new User();
            user = user.GetUserById(iduser);

            string message = "<H2>Unlock StrongBox for Heir</H2> <br><H2> Start of the process on :" + now + "</H2> <br><br>"
                             + " Customer<br>"
                             + " ID:" + cr.EncryptHexa(user.Id) + "<br>"
                             + " Firstname:" + user.FirstName + "<br>"
                             + " Lastname:" + user.LastName + "<br>"
                             + " Phone Number:" + user.PhoneNumber + "<br>"
                             + " Email:" + user.Email + "<br><br>"
                             + " StrongBox For Heir<br>"
                             + " ID:" + cr.EncryptHexa(item.id) + "<br>"
                             + " Label:" + item.label + "<br>"
                             + " Message For Heir:" + item.messageForHeirs + "<br>"
                             + " Wallet public Key:" + item.walletPublicKeyOwner + "<br>"
                             + " Secret public Key:" + item.secretPK + "<br>"
                             + " Solana public Key:" + item.solanaPK + "<br>"
                             + " Paying public Key:" + item.payingPK + "<br>"
                             + " Smart Contract public Key:" + item.scPK + "<br>"
                             + " Code ID:" + item.codeID + "<br>"
                             + " NFT shard Serenity public Key:" + item.solSereNftPK + "<br>"
                             + " NFT shard User public Key:" + item.solUsrNftPK + "<br>"
                             + " NFT shard Heir public Key::" + item.solHeirNftPKs + "<br><br>";

            string content = "Content<br>";

            if (item.content.walletDex.Count > 0)
            {
                content += "Wallet Dex<br>";
                foreach (var c in item.content.walletDex)
                {
                    if (c.type != null)
                    {

                        content += "Label:" + c.label + "<br>"
                                 + "Provider:" + c.provider + "<br>"
                                 + "Seed:" + c.seed + "<br>"
                                 + "Type Wallet<br>"
                                 + "Label:" + c.type.label + "<br>"
                                 + "type:" + c.type.type + "<br>"
                                 + "Supported:" + c.type.supported + "<br>";
                    }
                    else
                    {
                        content += "Label:" + c.label + "<br>"
                          + "Provider:" + c.provider + "<br>";

                    }

                }
            }
            if (item.content.walletCex.Count > 0)
            {
                content += "Wallet Cex<br>";
                foreach (var c in item.content.walletCex)
                {
                    if (c.type != null)
                    {

                        content += "Label:" + c.label + "<br>"
                                 + "Provider:" + c.provider + "<br>"
                                 + "Type Wallet<br>"
                                 + "Label:" + c.type.label + "<br>"
                                 + "type:" + c.type.type + "<br>"
                                 + "Supported:" + c.type.supported + "<br>";
                    }
                    else
                    {
                        content += "Label:" + c.label + "<br>"
                          + "Provider:" + c.provider + "<br>";


                    }

                }
            }
            if (item.content.walletDesktop.Count > 0)
            {
                content += "Wallet Desktop<br>";
                foreach (var c in item.content.walletDesktop)
                {
                    if (c.type != null)
                    {

                        content += "Label:" + c.label + "<br>"
                                 + "Provider:" + c.provider + "<br>"
                                 + "Type Wallet<br>"
                                 + "Label:" + c.type.label + "<br>"
                                 + "type:" + c.type.type + "<br>"
                                 + "Supported:" + c.type.supported + "<br>";
                    }
                    else
                    {
                        content += "Label:" + c.label + "<br>"
                          + "Provider:" + c.provider + "<br>";


                    }

                }
            }
            if (item.content.walletHardware.Count > 0)
            {
                content += "Wallet Harware<br>";
                foreach (var c in item.content.walletHardware)
                {
                    if (c.type != null)
                    {

                        content += "Label:" + c.label + "<br>"
                                 + "Provider:" + c.provider + "<br>"
                                 + "Type Wallet<br>"
                                 + "Label:" + c.type.label + "<br>"
                                 + "type:" + c.type.type + "<br>"
                                 + "Supported:" + c.type.supported + "<br>";
                    }
                    else
                    {
                        content += "Label:" + c.label + "<br>"
                          + "Provider:" + c.provider + "<br>";


                    }

                }
            }
            content += "<br>";

            string listHeir = " Heirs<br>";
            foreach (IHeir h in item.heirs)
            {
                listHeir += " Firstname:" + h.firstName + "<br>"
                             + " Lastname:" + h.lastName + "<br>"
                             + " Phone Number:" + h.phone + "<br>"
                             + " Email:" + h.email + "<br>"
                             + " Public Key:" + h.publicKey + "<br>"
                             + " Wallet Type:" + h.walletType.label + "<br>"
                             + " Creation date:" + h.creation_Date + "<br><br>";
            }



            using (MailMessage mailMsg = new MailMessage())
            {
                // API key
                string apiKey = Environment.GetEnvironmentVariable("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");

                // To
                mailMsg.To.Add(new MailAddress("policy-activity@serenityshield.io"));
                // mailMsg.To.Add(new MailAddress("Policy-activity@serenityshield.io"));

                // From
                mailMsg.From = new MailAddress("hostmaster@serenityshield.io", "Serenity Shield");
                // Subject and multipart/alternative Body
                mailMsg.Subject = "Period of inactivity exceeded";
                string text = message + content + listHeir;
                string html = message + content + listHeir;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                using (SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("apikey", "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");
                    smtpClient.Send(mailMsg);
                }
            }
        }
        #endregion email
        #region sms

        #endregion sms

    }
}
