using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace DrawerBackup.PerformanceVariables
{
    /// <summary>
    /// Performance Variable Manager
    /// </summary>
    public class PerfVarManager : IPerfVarManager
    {
        private string varsFile;
        private Collection<PerfValue> values = new Collection<PerfValue>( );
        private PerfVariable variable;

        public PerfVarManager(PerfVariable variable)
        {
            this.variable = variable;
            CheckFile(variable);
            Load( );
        }

        /// <summary>
        /// Clear all the values and remove the file
        /// </summary>
        public void Purge()
        {
            Clear( );
            Save( );
            File.Delete(varsFile);
        }

        /// <summary>
        /// Loads all values for the variable from the Disk
        /// </summary>
        public void Load( )
        {
            if (File.Exists(varsFile))
            {
                values = JsonConvert.DeserializeObject<Collection<PerfValue>>(File.ReadAllText(varsFile));
            }

            if (values == null)
                values = new Collection<PerformanceVariables.PerfValue>( );
        }

        /// <summary>
        /// Save all values to the Disk
        /// </summary>
        public void Save()
        {
            if (values == null)
                values = new Collection<PerformanceVariables.PerfValue>( );

            lock (values)
            {
                string json = JsonConvert.SerializeObject(values, Formatting.Indented);
                File.WriteAllText(varsFile, json);
            }
        }

        /// <summary>
        /// Clear all values in the variable
        /// </summary>
        public void Clear()
        {
            lock (values)
            {
                values.Clear( );
                Save( );
            }
        }

        /// <summary>
        /// Check and remove any expired value
        /// </summary>
        public void CheckExpiredValues()
        {
            if (values == null)
                return;

            lock (values)
            {
                var search = values.Where(p => ValidateValue(p));
                if (search.Any( ))
                {
                    foreach (var item in search.ToArray( ))
                    {
                        values.Remove(item);
                    }
                }

                Save( );
            }
        }

        /// <summary>
        /// Get the maximun value from the variable
        /// </summary>
        /// <returns></returns>
        public double Max()
        {
            if(values.Count > 0)
            {
                return values.Max(p => p.Value);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the minimun value from the variable
        /// </summary>
        /// <returns></returns>
        public double Min( )
        {
            if (values.Count > 0)
            {
                return values.Min(p => p.Value);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the average value from the variable
        /// </summary>
        /// <returns></returns>
        public double Avg( )
        {
            if (values.Count > 0)
            {
                return values.Average(p => p.Value);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the last value from the variable
        /// </summary>
        /// <returns></returns>
        public double First( )
        {
            if (values.Count > 0)
            {
                return values.OrderBy(p => p.At).First( ).Value;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the first value from the variable
        /// </summary>
        /// <returns></returns>
        public double Last( )
        {
            if (values.Count > 0)
            {
                return values.OrderByDescending(p => p.At).First( ).Value;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Insert a value to the performance variable
        /// </summary>
        /// <param name="value"></param>
        public void Insert(double value)
        {
            lock (values)
            {
                if (values.Count > 0)
                {
                    if (variable.Kind == PerfVarKind.Single)
                    {
                        InsertSingle(value);
                    }
                    else if (variable.Kind == PerfVarKind.Multiple)
                    {
                        InsertMultiple(value);
                    }
                }
                else
                {
                    values.Add(new PerformanceVariables.PerfValue( )
                    {
                        At = DateTime.Now,
                        Value = value
                    });
                }
            }
        }

        private void InsertMultiple(double value)
        {
            var search = values.Where(p => IsNotExpired(p));
            if (search.Any( ))
            {
                PerfValue pValue = search.First( );
                int index = values.IndexOf(pValue);

                pValue.Value = ( pValue.Value + ( value ) );
                values[index] = pValue;
            }
            else
            {
                values.Add(new PerformanceVariables.PerfValue( )
                {
                    At = DateTime.Now,
                    Value = value
                });
            }
        }

        private bool IsNotExpired(PerfValue pValue)
        {
            DateTime expireAt = pValue.At.Add(variable.Changetime);
            return expireAt > DateTime.Now;
        }

        private void InsertSingle(double value)
        {
            PerfValue pValue = values[0];            
            if (IsNotExpired(pValue))
            {
                pValue.Value = ( pValue.Value + ( value ) );
            }
            else
            {
                pValue.Value = value;
            }

            pValue.At = DateTime.Now;
            values[0] = pValue;
        }

        private bool ValidateValue(PerfValue value)
        {
            DateTime expireAt = value.At.Add(variable.Lifetime);
            return ( expireAt < DateTime.Now );
        }

        private void CheckFile(PerfVariable variable)
        {
            string varsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Performance\\Vars\\");
            if (Directory.Exists(varsDir) == false)
                Directory.CreateDirectory(varsDir);

            this.varsFile = Path.Combine(varsDir, variable.Name + ".pvr");
        }
    }
}
