using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawerBackup.Scripting
{
    [Serializable]
    public class ScriptReference
    {
        /// <summary>
        /// The Assembly Name
        /// </summary>
        [XmlAttribute]
        public string Assembly { get; set; }

        /// <summary>
        /// The Location Name
        /// </summary>
        [XmlAttribute]
        public string Location { get; set; }


        /// <summary>
        /// Loads the Scripts References
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
        public static Collection<ScriptReference> Load(string scriptPath)
        {
            Collection<ScriptReference> refs = new Collection<ScriptReference>( );
            XmlSerializer serializer = new XmlSerializer(typeof(Collection<ScriptReference>));

            string scriptName = Path.GetFileNameWithoutExtension(scriptPath);
            string directory = Path.GetDirectoryName(scriptPath);

            string refFile = Path.Combine(directory, scriptName + "-references.xml");

            if(File.Exists(refFile))
            {
                using (Stream stream = File.OpenRead(refFile))
                {
                    refs = (Collection<ScriptReference>) serializer.Deserialize(stream);
                }
            }

            return refs;
        }
    }
}
