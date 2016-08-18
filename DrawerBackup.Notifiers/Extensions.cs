using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerBackup.Notifiers
{
    internal static class Extensions
    {
        internal static void AddIfNotExists(this Dictionary<string,string> dic, string key, string value)
        {
            if (dic.ContainsKey(key) == false)
                dic.Add(key, value);
        }
    }
}
