using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.StorageFileSystem
{
    /// <summary>
    /// Contract to implemente a repository controller
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// The Repository Path
        /// </summary>
        protected string RepositoryPath { get; set; }

        public Repository(string repositoryPath )
        {
            this.RepositoryPath = repositoryPath;
        }

        /// <summary>
        /// Open the image
        /// </summary>
        /// <param name="name">Name of the Image</param>
        /// <returns></returns>
        public abstract Image Open(string name);

        /// <summary>
        /// Creates the image in the repository
        /// </summary>
        /// <param name="name">Name of the Image</param>
        public abstract void Create(string name);

        /// <summary>
        /// Delete the image from the repository
        /// </summary>
        /// <param name="name">Name of the Image</param>
        public abstract void Delete(string name);

        /// <summary>
        /// List all the images
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> ListImages( );

        /// <summary>
        /// Check if the image exists in the repository
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract bool Exists(string name);
    }
}
