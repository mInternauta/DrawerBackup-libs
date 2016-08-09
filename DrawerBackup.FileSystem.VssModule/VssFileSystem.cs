using DrawerBackup.Client.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DrawerBackup.Client.Modules.Builtin;
using Alphaleonis.Win32.Vss;

namespace DrawerBackup.FileSystem.VssModule
{
    public class VssFileSystem : IFileSystem
    {
        private BuiltinFileSystem builtin;
        private IVssImplementation oVSSImpl;

        public VssFileSystem()
        {
            if (OperatingSystemInfo.IsAtLeast(OSVersionName.WindowsServer2003))
            {
                if (Environment.Is64BitOperatingSystem == Environment.Is64BitProcess)
                {
                    this.oVSSImpl = VssUtils.LoadImplementation();
                    this.builtin = new BuiltinFileSystem();
                }

                else
                {
                    throw new VssUnsupportedContextException("The Volume Shadow Service Module must run on the same architecture as the operating system architecture.");
                }
            }
            else
            {
                throw new VssUnsupportedContextException("Unsupported operating system for the Volume Shadow");
            }
        }

        public void EnumerateFiles(string directory, Action<string> fileAction, bool includeSubFolder = true)
        {
            this.builtin.EnumerateFiles(directory, fileAction, includeSubFolder);
        }

        public Stream OpenRead(string filename)
        {
            return Alphaleonis.Win32.Filesystem.File.OpenRead(filename);
        }

        public Stream OpenWrite(string filename)
        {
            return this.builtin.OpenWrite(filename);
        }
    }
}
