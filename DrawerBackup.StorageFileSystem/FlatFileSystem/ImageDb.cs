using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawerBackup.StorageFileSystem.FlatFileSystem
{
    /// <summary>
    /// A Simple database for store information about the images
    /// </summary>
    public class ImageDb : ICollection<FlatImageInfo>
    {
        private Collection<FlatImageInfo> dbase 
            = new Collection<FlatImageInfo>( );
        private string dbFile;
        private XmlSerializer serializer;

        public int Count
        {
            get
            {
                return dbase.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ImageDb(string repositoryPath)
        {
            this.serializer = new XmlSerializer(typeof(Collection<FlatImageInfo>));
            this.dbFile = Path.Combine(repositoryPath, "imagedb.xml");
            if(File.Exists(dbFile))
            {
                Load( );
            }
            else
            {
                Save( );
            }
        }

        public void Save( )
        {
            lock (dbase)
            {
                using (Stream wr = File.Open(dbFile, FileMode.Create))
                {
                    serializer.Serialize(wr, this.dbase);
                }
            }
        }

        public void Load( )
        {
            lock (dbase)
            {
                using (Stream rd = File.Open(dbFile, FileMode.Open))
                {
                    this.dbase = (Collection<FlatImageInfo>) serializer.Deserialize(rd);
                }
            }
        }

        public void Add(FlatImageInfo item)
        {
            this.dbase.Add(item);
            this.Save( );
        }

        public void Clear( )
        {
            this.dbase.Clear( );
            this.Save( );
        }

        public bool Contains(FlatImageInfo item)
        {
            return this.dbase.Contains(item);
        }

        public void CopyTo(FlatImageInfo[] array, int arrayIndex)
        {
            this.dbase.CopyTo(array, arrayIndex);
        }

        public bool Remove(FlatImageInfo item)
        {
            bool removed = this.dbase.Remove(item);
            this.Save( );
            return removed;
        }

        public IEnumerator<FlatImageInfo> GetEnumerator( )
        {
            return this.dbase.GetEnumerator( );
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return this.dbase.GetEnumerator( );
        }
    }
}
