using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Scripting
{
    /// <summary>
    /// Manage the directory of scripts
    /// </summary>
    public class ScriptDirectory
    {
        private string scriptDir;

        public ScriptDirectory( )
        {
            this.scriptDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts\\");
            if (Directory.Exists(scriptDir) == false)
                Directory.CreateDirectory(scriptDir);
        }

        /// <summary>
        /// Load the script from the directory
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        public string Load(string scriptName)
        {
            string path = GetScriptPath(scriptName);
            if (File.Exists(path))
                return File.ReadAllText(path);

            return "";
        }

        /// <summary>
        /// Save a script into the directory
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="code"></param>
        public void Save(string scriptName, string code)
        {
            string path = GetScriptPath(scriptName);
            File.WriteAllText(path, code);
        }

        /// <summary>
        /// Deletes a script from the directory
        /// </summary>
        /// <param name="scriptName"></param>
        public void Delete(string scriptName)
        {
            if (File.Exists(GetScriptPath(scriptName)))
                File.Delete(GetScriptPath(scriptName));
        }

        /// <summary>
        /// Get the compiler for the script
        /// </summary>
        /// <param name="scriptName">The name of the script</param>
        /// <returns></returns>
        public ScriptCompiler Compiler(string scriptName)
        {
            string scriptPath = GetScriptPath(scriptName);

            return new Scripting.ScriptCompiler(Path.GetFileNameWithoutExtension(scriptPath),
                File.Open(scriptPath, FileMode.Open));
        }

        /// <summary>
        /// Enumerate all avaliable scripts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> All()
        {
            return Directory
                .EnumerateFiles(scriptDir, "*.scs")
                .Select(p => Path.GetFileNameWithoutExtension(p));
        }


        private string GetScriptPath(string scriptName)
        {
            return Path.Combine(scriptDir, scriptName + ".scs");
        }


    }
}
