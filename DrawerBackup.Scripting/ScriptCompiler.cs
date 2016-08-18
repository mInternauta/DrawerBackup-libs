using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace DrawerBackup.Scripting
{
    /// <summary>
    /// Compile and execute any script
    /// </summary>
    public class ScriptCompiler
    {
        private string scriptId;
        private Stream stream;
        private bool compiled;
        private Assembly assembly;

        public ScriptCompiler(string id, Stream stream)
        {
            this.scriptId = id;
            this.stream = stream;

            References = new List<string>( );
        }

        /// <summary>
        /// References
        /// </summary>
        public List<string> References { get; set; }

        /// <summary>
        /// Get a object instance from the script which represents the main point
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public TInterface MainPoint<TInterface>( )
        {
            TInterface obj = default(TInterface);

            // Compile
            Compile( );

            // Load the assemnly
            LoadAssembly( );

            var search = assembly
                    .GetTypes( )
                    .Where(p => p.GetInterface(typeof(TInterface).Name, true) != null);
            if (search.Any( ))
            {
                obj = (TInterface) Activator.CreateInstance(search.First( ));
            }
            else
            {
                throw new EntryPointNotFoundException("Cant found a type who's implements: " + typeof(TInterface).Name);
            }

            return obj;
        }

        private void LoadAssembly( )
        {
            if (this.assembly == null)
            {
                using (MemoryStream assmContent = new MemoryStream( ))
                {
                    using (Stream assemblyFile = File.Open(getScriptPath( ), FileMode.Open))
                    {
                        assemblyFile.CopyTo(assmContent);
                    }
                    this.assembly = Assembly.Load(assmContent.ToArray( ));
                }
            }
        }

        /// <summary>
        /// Force the script to compile
        /// </summary>
        public void Recompile()
        {
            CompileScript( );
        }

        /// <summary>
        /// Compile the Script
        /// </summary>
        public void Compile()
        {
            if (!compiled)
                CompileScript( );
        }

        private void CompileScript()
        {
            CompilerResults results = CompileCode( );
            if(results.Errors.Count != 0)
            {
                ThrowScriptError(results);
            }
            else
            {
                this.assembly = null;
                this.compiled = true;

                LoadAssembly( );
            }
        }

        private CompilerResults CompileCode()
        {
            string scriptPath = getScriptPath( );
            string code = getCode( );

            CompilerResults results = null;

            CSharpCodeProvider provider = new CSharpCodeProvider( );
            CompilerParameters parameters = new CompilerParameters( );

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly( ).Location);

            foreach (string reference in References)
            {
                parameters.ReferencedAssemblies.Add(reference);
            }

            parameters.GenerateInMemory = false;
            parameters.OutputAssembly = scriptPath;
            results = provider.CompileAssemblyFromSource(parameters, code);

            return results;
        }

        private string getCode( )
        {
            string code = "";

            using (StreamReader rd = new StreamReader(stream))
            {
                code = rd.ReadToEnd( );
            }

            return code;
        }

        private string getScriptPath( )
        {
            string scriptBinDir = ScriptDir( );

            string scriptPath = Path.Combine(scriptBinDir, scriptId + ".dll");
            return scriptPath;
        }

        private static string ScriptDir( )
        {
            string scriptBinDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts\\Compiled\\");
            if (Directory.Exists(scriptBinDir) == false)
                Directory.CreateDirectory(scriptBinDir);
            return scriptBinDir;
        }

        private void ThrowScriptError(CompilerResults cr)
        {
            string message = "";
            foreach (CompilerError err in cr.Errors.OfType<CompilerError>( ))
            {
                message += err.ToString( ) + "\r\n";
            }

            throw new InvalidDataException(message);
        }
    }
}
