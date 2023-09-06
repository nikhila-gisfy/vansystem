using System;
using System.Net;
using System.Net.Mail;

namespace vansystem
{
    public class SendEmail
    {
        public string sendEmailMsg(string subject, string attachments, string body, string toMailAddress)
        {
            string response = string.Empty;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("girijapriyadarsini6@gmail.com");
                message.To.Add(new MailAddress(toMailAddress));
                message.Subject = subject;

                if (!string.IsNullOrEmpty(attachments))
                {
                    Attachment Newattachment;
                    Newattachment = new Attachment(attachments);
                    message.Attachments.Add(Newattachment);
                }

                message.IsBodyHtml = true; //to make message body as html
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("girijapriyadarsini6@gmail.com", "kkktrpmlytnidrqh");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                response = "Success";
            }
            catch (Exception ex)
            {
                response = "Error Occured";
            }

            return response;
        }
    }
}