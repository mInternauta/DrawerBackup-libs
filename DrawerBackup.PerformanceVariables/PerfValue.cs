using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.PerformanceVariables
{
    /// <summary>
    /// Its the value for a performance variable
    /// </summary>
    public class PerfValue
    {
        /// <summary>
        /// The Variable Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The time the variable was inserted or updated
        /// </summary>
        public DateTime At { get; set; }
    }
}
