using System.Net.Mail;

namespace MinimalApi.Models
{

    public class MailAccountInfo
    {
        public  string host { get; set; }
        public string EmailAddress { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
    public class MailHelper
    {
        public bool SendEmail(string otp, MailAccountInfo accountInfo,string ToName,string ToMailAddr = "kmhridoynub@gmail.com")
        {
            try
            {
                MailMessage m = new MailMessage(
                        new MailAddress(accountInfo.EmailAddress, accountInfo.FromName),
                        new MailAddress(ToMailAddr));
                m.Subject = "Email varification";
                m.Body = string.Format($"Dear {ToName} <BR/>Your verification Code is {otp} ");
                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(accountInfo.EmailAddress, accountInfo.Password);
                smtp.EnableSsl = accountInfo.EnableSSL;
                smtp.UseDefaultCredentials = false;
                smtp.Port = accountInfo.Port;
                smtp.Send(m);
                return true;
            }
            catch (Exception Ex)
            {
                var msg = Ex.Message;
                return false;
            }

        }


    }
}
