using System;

namespace Brainf
{
    /// <summary>
    /// Helper class for working with implementations of the <see cref="IBrainfCompiler"/>.
    /// </summary>
    public static class BrainfCompilerExtensions
    {
        /// <summary>
        /// Compiles a program written in `Brainfuck` into a function.
        /// </summary>
        /// <param name="compiler">Implementation of the <see cref="IBrainfCompiler"/>.</param>
        /// <param name="program">`Brainfuck` program.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="compiler"/> parameter is null.
        ///     The <paramref name="program"/> parameter is null.
        /// </exception>
        /// <seealso cref="IBrainfParser.Parse"/>
        /// <seealso cref="IBrainfStream"/>
        /// <example>
        /// <code>
        ///     const string sourceCode = ...;
        ///  
        ///     var parser = BrainfParser.Default;
        ///     var compiler = BrainfCompiler.Default;
        ///  
        ///     var program = parser.Parse(sourceCode);
        ///     var func = compiler.Compile(program);
        ///  
        ///     func(KnownBrainfStreams.Console);   
        /// </code>
        /// </example>
        public static Action<IBrainfStream> Compile(this IBrainfCompiler compiler, IBrainfProgram program)
        {
            if (compiler == null) 
                throw new ArgumentNullException(nameof(compiler));
            if (program == null)
                throw new ArgumentNullException(nameof(program));

            return compiler.Compile<BrainfMemory>(program);
        }
    }
}