using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers.Builtin.Email
{
    /// <summary>
    /// Notifier for Emails
    /// </summary>
    public class EmailNotifier : INotifier
    {
        private SmtpClient client;
        private EmailSettings settings;

        public EmailNotifier( )
        {
            this.settings = ConfigManager.Load<EmailSettings>( );
            this.client = new SmtpClient(settings.SmtpServer, settings.SmtpPort);
            this.client.EnableSsl = settings.SmtpUseSsl;
            this.client.Credentials = new NetworkCredential(settings.SmtpUser, settings.SmtpPassword);
        }

        public void Notify(Notification notification)
        {
            MailMessage mail = new MailMessage(settings.From, settings.To);
            mail.Subject = "[" + Enum.GetName(typeof(NotificationKind), notification.Kind) + "]"
                + notification.Description;
            mail.IsBodyHtml = true;
            mail.Body = getBody(notification);

            client.Send(mail);
        }

        private string getBody(Notification notification)
        {
            // Loads the template //
            string templatePath = getTemplatePath( );
            checkTemplate(templatePath);

            string template = File.ReadAllText(templatePath);

            string body = notification.Body.Replace("\r\n", "<br />");
            template = template.Replace("{DESCRIPTION}", notification.Description);
            template = template.Replace("{BODY}", body);

            return template;
        }

        private void checkTemplate(string templatePath)
        {
            if (File.Exists(templatePath) == false)
            {
                using (StreamWriter wr = new StreamWriter(File.OpenWrite(templatePath)))
                using (StreamReader rd = new StreamReader(
                    Assembly.GetExecutingAssembly( )
                    .GetManifestResourceStream("DrawerBackup.Notifiers.Builtin.Email.Templates.Email.html")))
                {
                    wr.Write(rd.ReadToEnd( ));
                    wr.Flush( );
                }
            }
        }

        private string getTemplatePath( )
        {
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notifiers\\Email\\");
            if (Directory.Exists(templatePath) == false)
                Directory.CreateDirectory(templatePath);
            templatePath = Path.Combine(templatePath, "Email.html");
            return templatePath;
        }

        public Dictionary<string, string> GetSettings( )
        {
            return this.settings;
        }

        public void SetSettings(Dictionary<string, string> cfg)
        {
            foreach (var set in cfg)
            {
                this.settings[set.Key] = set.Value;
            }
            ConfigManager.Save(this.settings);
        }
    }
}
