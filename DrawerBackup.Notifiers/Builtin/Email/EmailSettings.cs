using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers.Builtin.Email
{
    public class EmailSettings : Dictionary<string,string>
    {
        /// <summary>
        /// Use SSL for the Smtp Server
        /// </summary>
        public bool SmtpUseSsl
        {
            get
            {
                return bool.Parse(this["SmtpUseSsl"]);
            }
            set
            {
                this["SmtpUseSsl"] = value.ToString( );
            }
        }

        /// <summary>
        /// The port of the Smtp Server
        /// </summary>
        public int SmtpPort
        {
            get
            {
                return int.Parse(this["SmtpPort"]);
            }
            set
            {
                this["SmtpPort"] = value.ToString();
            }
        }

        /// <summary>
        /// The Server address
        /// </summary>
        public string SmtpServer
        {
            get
            {
                return this["SmtpServer"];
            }
            set
            {
                this["SmtpServer"] = value;
            }
        }

        /// <summary>
        /// The from field of the email
        /// </summary>
        public string From
        {
            get
            {
                return this["From"];
            }
            set
            {
                this["From"] = value;
            }
        }

        /// <summary>
        /// The to field of the email
        /// </summary>
        public string To
        {
            get
            {
                return this["To"];
            }
            set
            {
                this["To"] = value;
            }
        }

        /// <summary>
        /// The Server username
        /// </summary>
        public string SmtpUser
        {
            get
            {
                return this["SmtpUser"];
            }
            set
            {
                this["SmtpUser"] = value;
            }
        }

        /// <summary>
        /// The Server Password
        /// </summary>
        public string SmtpPassword
        {
            get
            {
                return this["SmtpPassword"];
            }
            set
            {
                this["SmtpPassword"] = value;
            }
        }
    }
}
