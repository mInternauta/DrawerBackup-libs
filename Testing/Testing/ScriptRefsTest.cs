using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DrawerBackup.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ScriptRefsTest
    {
        [TestMethod]
        public void TestRefsXml()
        {
            String xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Collection<ScriptReference>));
            Collection<ScriptReference> items = new Collection<ScriptReference>( );

            items.Add(new ScriptReference( )
            {
                 Assembly = "Teste",
                 Location = "Teste.dll"
            });

            using (StringWriter wr = new StringWriter())
            {
                serializer.Serialize(wr, items);
                wr.Flush( );

                xml = wr.ToString( );
            }
        }
    }
}
