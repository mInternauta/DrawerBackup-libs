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
        /// <param name="Id">Id of the Image</param>
        /// <returns></returns>
        public abstract Image Open(string id);

        /// <summary>
        /// Creates the image in the repository
        /// </summary>
        /// <param name="name">Name of the Image</param>
        /// <param name="clientName">Name of the Client</param>
        public abstract string Create(string name, string clientName);

        /// <summary>
        /// Delete the image from the repository
        /// </summary>
        /// <param name="name">Id of the Image</param>
        public abstract void Delete(string id);

        /// <summary>
        /// List all the images
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> ListImages( );

        /// <summary>
        /// Get the repository size
        /// </summary>
        /// <returns></returns>
        public abstract long Size( );

        
        /// <summary>
        /// Check if the image exists in the repository
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract bool Exists(string name);
    }
}
