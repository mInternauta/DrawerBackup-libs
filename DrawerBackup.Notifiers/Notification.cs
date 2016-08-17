using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers
{
    public class Notification
    {
        /// <summary>
        /// Body of the Notification
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Notification Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Kind of the Notification
        /// </summary>
        public NotificationKind Kind { get; set; }
    }
}
