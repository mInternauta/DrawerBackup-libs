using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DrawerBackup.Notifiers
{
    /// <summary>
    /// Manages the configuration
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// Loads the Configuration
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        public static TConfig Load<TConfig>() where TConfig : IDictionary<String,String>
        {
            TConfig config = (TConfig) Activator.CreateInstance(typeof(TConfig));

            string configPath = GetConfigPath<TConfig>( );

            if (File.Exists(configPath))
            {
                config = (TConfig) JsonConvert.DeserializeObject<TConfig>(File.ReadAllText(configPath));
            }

            return config;
        }

        /// <summary>
        /// Loads the Configuration
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <returns></returns>
        public static void Save<TConfig>(TConfig config) where TConfig : IDictionary<String, String>
        {            
            string configPath = GetConfigPath<TConfig>( );
            string json = JsonConvert.SerializeObject(config);

            File.WriteAllText(configPath, json);
        }

        private static string GetConfigPath<TConfig>( ) where TConfig : IDictionary<string, string>
        {
            string configName = typeof(TConfig).Name;
            string configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Notifiers\\");
            if (Directory.Exists(configDir) == false)
                Directory.CreateDirectory(configDir);

            string configPath = Path.Combine(configDir, configName + ".jcfg");
            return configPath;
        }
    }
}
