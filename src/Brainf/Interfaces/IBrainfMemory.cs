namespace Brainf
{
    /// <summary>
    /// Interface used for implementing memory usage algorithm.
    /// </summary>
    public interface IBrainfMemory
    {
        /// <summary>
        /// Gets the total number of memory cells.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Gets or sets the current memory pointer.
        /// </summary>
        /// <seealso cref="CellValue"/>
        /// <example>
        /// <code>
        ///     const string sourceCode = ...;
        ///  
        ///     var memory = new BrainfMemory();
        ///  
        ///     memory.Pointer++;
        ///     memory.CellValue = 1;
        ///  
        ///     memory.Pointer++;
        ///     memory.CellValue = 2;
        ///  
        ///     Console.WriteLine(memory.CellValue); // 2
        ///  
        ///     memory.Pointer--;
        ///  
        ///     Console.WriteLine(memory.CellValue); // 1
        /// </code>
        /// </example>
        int Pointer { get; set; }

        /// <summary>
        /// Gets or sets the current memory cell value.
        /// </summary>
        /// <seealso cref="Pointer"/>
        int CellValue { get; set; }
    }
}
