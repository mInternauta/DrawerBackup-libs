namespace DrawerBackup.PerformanceVariables
{
    /// <summary>
    /// Performance Variable Manager
    /// </summary>
    public interface IPerfVarManager
    {
        /// <summary>
        /// Clear the variable
        /// </summary>
        void Clear( );

        /// <summary>
        /// Gets the Average value 
        /// </summary>
        /// <returns></returns>
        double Avg( );

        /// <summary>
        /// Gets the first value
        /// </summary>
        /// <returns></returns>
        double First( );

        /// <summary>
        /// Inserts a value to the variable
        /// </summary>
        /// <param name="value"></param>
        void Insert(double value);

        /// <summary>
        /// Gets the last value
        /// </summary>
        /// <returns></returns>
        double Last( );

        /// <summary>
        /// Get the maximun value
        /// </summary>
        /// <returns></returns>
        double Max( );

        /// <summary>
        /// Get the minimun value
        /// </summary>
        /// <returns></returns>
        double Min( );
    }
}