using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrawerBackup.Notifiers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class NotifierTest
    {
        [TestMethod]
        public void TestNotify()
        {
            NotifierFactory.Notify(new Notification( )
            {
                Body = "Test Notification\r\nTest Line",
                Description = "Test",
                Kind = NotificationKind.Error
            });
        }
    }
}
