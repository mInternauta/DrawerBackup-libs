using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DrawerBackup.PerformanceVariables
{
    /// <summary>
    /// Manage all variables in the current application
    /// </summary>
    public class PerfFactory
    {
        private Collection<PerfVariable> variables = new Collection<PerfVariable>( );
        private string file;
        private bool runFactory;
        private Thread factoryThread;
        private Dictionary<string, PerfVarManager> managers;

        public PerfFactory( )
        {
            CheckFile();
            Load( );
        }

        /// <summary>
        /// Gets the manager for a variable
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public IPerfVarManager Get(string variableName)
        {
            if (managers.ContainsKey(variableName))
                return managers[variableName];
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Stop the factory
        /// </summary>
        public void Stop()
        {
            this.runFactory = false;
            if (this.factoryThread != null && this.factoryThread.IsAlive)
                this.factoryThread.Join( );
        }

        /// <summary>
        /// Remove a variable
        /// </summary>
        /// <param name="variableName"></param>
        public void Remove(string variableName)
        {
            var search = variables.Where(p => p.Name == variableName);
            if (search.Any( ))
            {
                variables.Remove(search.First( ));
            }

            if (managers.ContainsKey(variableName))
            {
                managers[variableName].Purge( );
                managers.Remove(variableName);
            }
        }

        /// <summary>
        /// Start the factory
        /// </summary>
        public void Start()
        {
            this.runFactory = true;

            // Loads all performance variables managers
            LoadManagers( );

            this.factoryThread = new Thread(new ThreadStart(_FactoryMain));
            this.factoryThread.Start( );
        }


        /// <summary>
        /// Gets all configured variables
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PerfVariable> All()
        {
            return variables;
        }

        /// <summary>
        /// Update the variable
        /// </summary>
        /// <param name="variable"></param>
        public void Update(PerfVariable variable)
        {
            Remove(variable);
            variables.Add(variable);
            CheckManager(variable);


            Save( );
        }

        private void CheckManager(PerfVariable variable)
        {
            if(managers.ContainsKey(variable.Name) == false)
            {
                managers.Add(variable.Name, new PerfVarManager(variable));
            }
        }

        /// <summary>
        /// Remove the variable
        /// </summary>
        /// <param name="variable"></param>
        public void Remove(PerfVariable variable)
        {
            Remove(variable.Name);

            Save( );
        }
        
        private void _FactoryMain( )
        {
            while (this.runFactory)
            {
                var list = managers.ToArray( );  
                foreach (var manager in list)
                {
                    manager.Value.CheckExpiredValues( );
                    manager.Value.Save( );
                }

                Thread.Sleep(2000);
            }
        }

        private void LoadManagers( )
        {
            this.managers = new Dictionary<string, PerfVarManager>( );
            foreach (PerfVariable variable in variables)
            {
                PerfVarManager manager = new PerfVarManager(variable);
                managers.Add(variable.Name, manager);
            }
        }

        private void Load( )
        {
            if(File.Exists(file))
            {
                variables = JsonConvert.DeserializeObject<Collection<PerfVariable>>(File.ReadAllText(file));
            }
        }

        private void Save()
        {
            string json = JsonConvert.SerializeObject(variables, Formatting.Indented);
            File.WriteAllText(file, json);
        }

        private void CheckFile( )
        {
            string varsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Performance\\");
            if (Directory.Exists(varsDir) == false)
                Directory.CreateDirectory(varsDir);

            this.file = Path.Combine(varsDir, "performance.set");
        }
    }
}
