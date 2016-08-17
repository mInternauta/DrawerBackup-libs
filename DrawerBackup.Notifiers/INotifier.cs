using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers
{
    /// <summary>
    /// Contract to implement a notifier
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// Notify the user 
        /// </summary>
        /// <param name="notification"></param>
        void Notify(Notification notification);
    }
}
