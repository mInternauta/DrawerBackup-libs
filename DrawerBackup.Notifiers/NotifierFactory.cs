using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers
{
    /// <summary>
    /// Notifier Factory
    /// </summary>
    public static class NotifierFactory
    {
        /// <summary>
        /// The last exception occurred
        /// </summary>
        public static Exception LastException { get; private set; }

        /// <summary>
        /// Sends the notification
        /// </summary>
        /// <param name="notification"></param>
        public static void Notify(Notification notification)
        {
            LastException = null;

            try
            {
                NotiferSettings settings = ConfigManager.Load<NotiferSettings>( );
                Type type = Type.GetType(settings.NotifierType, true);

                INotifier notifier = (INotifier)Activator.CreateInstance(type);

                notifier.Notify(notification);

                ConfigManager.Save(settings);
            }
            catch (Exception exp)
            {
                LastException = exp;
            }
        }
    }
}
