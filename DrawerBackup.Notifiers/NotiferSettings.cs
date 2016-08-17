using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawerBackup.Notifiers.Builtin.Email;

namespace DrawerBackup.Notifiers
{
    /// <summary>
    /// Settings for the Notifier
    /// </summary>
    public class NotiferSettings : Dictionary<string,string>
    {
        public NotiferSettings( )
        {
            if (this.ContainsKey("NotifierType") == false)
                this["NotifierType"] = typeof(EmailNotifier).FullName;
        }

        public string NotifierType
        {
            get
            {
                return this["NotifierType"];
            }
            set
            {
                this["NotiferType"] = value;
            }
        }
    }
}
