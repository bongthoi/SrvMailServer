using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;

namespace SrvMailServer
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public Boolean SendMailToCustomer(String tomail,String body,String subject)
        {
            try
            {
                SmtpClient client = new SmtpClient("mail.vivmall.vn");
                String username = "contact@vivmall.vn";
                String password = "123456";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(username, password);
                client.Port = 587;
                client.EnableSsl = true;

                MailMessage maili = new MailMessage();
                maili.Body = body;
                maili.Subject = subject;
                maili.IsBodyHtml = true;
                maili.From = new MailAddress(username);
                maili.To.Add(tomail);

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    client.Send(maili);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                maili.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
