using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BreathTechRelease.Models
{
    public class Email
    {

        public Email(string fromemail, string toemail, string subject, string msgBody)
        {

                string Subject = subject;
                string MessaseBody = msgBody;
                string Email = fromemail;
                string ToEmail = toemail;

                try
                {

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(Email);
                    mail.To.Add(ToEmail);
                    mail.Subject = Subject;
                    mail.Body = MessaseBody;

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = false;
                    SmtpServer.UseDefaultCredentials = true;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Email, "PleaseCuddleTo3s!");

                    SmtpServer.Send(mail);
                    App.Current.MainPage.DisplayAlert("Notification", "Email has been sent to :" + ToEmail, " okay");
                }
                catch (Exception ex)
                {
                   App.Current.MainPage.DisplayAlert("Email Sending Failed", ex.Message, "OK");
                }
            }
    }
}


