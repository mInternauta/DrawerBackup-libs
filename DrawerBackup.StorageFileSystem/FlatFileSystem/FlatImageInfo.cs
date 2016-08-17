using System;

namespace DrawerBackup.StorageFileSystem.FlatFileSystem
{
    public class FlatImageInfo
    {
        public FlatImageInfo( )
        {
            Id = Guid.NewGuid( ).ToString("N");
        }

        /// <summary>
        /// Id of the Image
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the image
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Relative Path to the Repository
        /// </summary>
        public string Relativepath { get; set; }
    }
}