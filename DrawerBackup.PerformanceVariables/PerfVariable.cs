using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.PerformanceVariables
{
    /// <summary>
    /// Defines a Performance Variable
    /// </summary>
    public class PerfVariable
    {
        /// <summary>
        /// Name of the Performance Variable
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Kind of the Performance Variable
        /// </summary>
        public PerfVarKind Kind { get; set; }

        /// <summary>
        /// Lifetime of a value of the performance variables
        /// </summary>
        public TimeSpan Lifetime { get; set; }

        /// <summary>
        /// The maximun change time of a value of the performance variables
        /// </summary>
        public TimeSpan Changetime { get; set; }
    }
}
