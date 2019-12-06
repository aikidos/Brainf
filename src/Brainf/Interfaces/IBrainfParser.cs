using System;

namespace Brainf
{
    /// <summary>
    /// Interface used for implementing `Brainfuck` source code parser.
    /// </summary>
    public interface IBrainfParser
    {
        /// <summary>
        /// Returns `Brainfuck` program from source code.
        /// </summary>
        /// <param name="sourceCode">Source code.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="sourceCode"/> parameter is null.
        /// </exception>
        /// <example>
        /// <code>
        ///     const string sourceCode = ...;
        ///  
        ///     var parser = BrainfParser.Default;
        ///  
        ///     var program = parser.Parse(sourceCode);
        /// </code>
        /// </example>
        IBrainfProgram Parse(string sourceCode);
    }
}
