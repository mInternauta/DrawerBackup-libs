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
        /// Get the settings of the current notifier
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,string> GetNotifierSettings()
        {
            INotifier notifier = getNotifier( );
            return notifier.GetSettings( );
        }

        /// <summary>
        /// Set the settings of the current notifier
        /// </summary>
        /// <param name="settings"></param>
        public static void SetNotifierSettings(Dictionary<string,string> settings)
        {
            INotifier notifier = getNotifier( );
            notifier.SetSettings(settings);
        }

        /// <summary>
        /// Sends the notification
        /// </summary>
        /// <param name="notification"></param>
        public static void Notify(Notification notification)
        {
            LastException = null;

            try
            {
                INotifier notifier = getNotifier( );

                notifier.Notify(notification);
            }
            catch (Exception exp)
            {
                LastException = exp;
            }
        }

        private static INotifier getNotifier( )
        {
            NotiferSettings settings = ConfigManager.Load<NotiferSettings>( );
            ConfigManager.Save(settings);

            Type type = Type.GetType(settings.NotifierType, true);

            INotifier notifier = (INotifier) Activator.CreateInstance(type);
            return notifier;
        }
    }
}
