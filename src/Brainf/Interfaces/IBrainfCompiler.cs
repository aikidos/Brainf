using System;

namespace Brainf
{
    /// <summary>
    /// Interface used for implementing `Brainfuck` compiler.
    /// </summary>
    public interface IBrainfCompiler
    {
        /// <summary>
        /// Compiles a program written in `Brainfuck` into a function.
        /// </summary>
        /// <param name="program">`Brainfuck` program.</param>
        /// <typeparam name="TMemory">Type of implementation of the <see cref="IBrainfMemory"/>.</typeparam>
        /// <exception cref="ArgumentNullException">
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
        ///     var func = compiler.Compile&lt;BrainfMemory&gt;(program);
        ///  
        ///     func(KnownBrainfStreams.Console);   
        /// </code>
        /// </example>
        Action<IBrainfStream> Compile<TMemory>(IBrainfProgram program)
            where TMemory : IBrainfMemory, new();
    }
}
