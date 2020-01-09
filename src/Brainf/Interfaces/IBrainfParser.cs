using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Brainf.Exceptions;

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
        /// <exception cref="BrainfParseException">
        ///     The <paramref name="sourceCode"/> parameter contains syntax errors.
        /// </exception>
        /// <seealso cref="TryParse"/>
        /// <example>
        /// <code>
        ///     const string sourceCode = ...;
        ///  
        ///     var parser = BrainfParser.Default;
        ///  
        ///     var program = parser.Parse(sourceCode);
        /// </code>
        /// </example>
        [Pure]
        IBrainfProgram Parse(string sourceCode);

        /// <summary>
        /// Returns `Brainfuck` program from source code.
        /// A return value indicates whether the parsing operation succeeded.
        /// </summary>
        /// <param name="sourceCode">Source code.</param>
        /// <param name="program">`Brainfuck` program.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="sourceCode"/> parameter is null.
        /// </exception>
        /// <example>
        /// <code>
        ///     const string sourceCode = ...;
        ///  
        ///     var parser = BrainfParser.Default;
        ///  
        ///     if (!parser.TryParse(sourceCode, out var program, out var errorMessage))
        ///     {
        ///         Console.WriteLine($"Error! {errorMessage}");
        ///         return;
        ///     }
        /// </code>
        /// </example>
        [Pure]
        bool TryParse(string sourceCode, 
            [NotNullWhen(true)] out IBrainfProgram? program, 
            [NotNullWhen(false)] out string? errorMessage);
    }
}
