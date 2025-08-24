using System; using System.Net.Mail; using System.Configuration; using System.IO;
public static class EmailService {
    public static string SendMail(string to, string subj, string bodyHtml, byte[] attachment=null, string attachName=null) {
        try {
            var host = ConfigurationManager.AppSettings["SmtpHost"];
            if(string.IsNullOrEmpty(host)) return "SMTP not configured";
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]??"587"); var user=ConfigurationManager.AppSettings["SmtpUser"]; var pass=ConfigurationManager.AppSettings["SmtpPass"]; var from = ConfigurationManager.AppSettings["SmtpFrom"]??"no-reply@jbvnl.local";
            var msg = new MailMessage(); msg.From = new MailAddress(from,"JBVNL"); msg.To.Add(to); msg.Subject = subj; msg.IsBodyHtml = true; msg.Body = bodyHtml;
            if(attachment!=null){ var ms=new MemoryStream(attachment); msg.Attachments.Add(new Attachment(ms, attachName)); }
            var smtp = new SmtpClient(host, port); smtp.EnableSsl = true; if(!string.IsNullOrEmpty(user)) smtp.Credentials = new System.Net.NetworkCredential(user, pass); smtp.Send(msg);
            return "OK";
        } catch(Exception ex) { return "Email error: " + ex.Message; }
    }
}