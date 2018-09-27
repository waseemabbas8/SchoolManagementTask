using SchoolManagementTask.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SchoolManagementTask.Helpers
{
    public static class NotificationHandler
    {
        public static string SendEmail(MailMessage email)
        {
            string ResponseMessage="";

            email.IsBodyHtml = true;

            //smtp object
            SmtpClient oSMTP = new SmtpClient();
            oSMTP.Host = EmailIfo.SMTP_HOST_GOOGLE;
            oSMTP.Port = Convert.ToInt32(EmailIfo.SMTP_PORT_GOOGLE); //465 //25
            oSMTP.EnableSsl = true;
            oSMTP.Credentials = new System.Net.NetworkCredential(EmailIfo.SMTP_USER_NAME, EmailIfo.SMTP_PASSWORD);

            try
            {
                oSMTP.Send(email);
                ResponseMessage = "Email Sent Succefully";
            }
            catch (Exception ex)
            {
                ResponseMessage = "Could not send Email, Some Error occoured while completing operation";
            }

            return ResponseMessage;
        }
    }
}
