using System;
using System.Diagnostics.Contracts;
using Brainf.Memory;
using Brainf.Parser;
using Brainf.Program;
using Brainf.Streams;

namespace Brainf.Compiler;

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
    /// <typeparam name="TStream">Type of implementation of the <see cref="IBrainfStream"/>.</typeparam>
    /// <exception cref="ArgumentNullException">
    ///     The <paramref name="program"/> parameter is null.
    /// </exception>
    /// <seealso cref="IBrainfParser.Parse"/>
    /// <seealso cref="IBrainfStream"/>
    /// <example>
    /// <code>
    ///     const string sourceCode = ...;
    ///
    ///     var parser = new BrainfParser();
    ///     var compiler = new BrainfCompiler();
    ///
    ///     var program = parser.Parse(sourceCode);
    ///
    ///     var func = compiler.Compile&lt;BrainfMemory, ConsoleBrainfStream&gt;(program);
    ///
    ///     var memory = new BrainfMemory();
    ///     var stream = BrainfStreams.Console;
    ///
    ///     func(memory, stream);
    /// </code>
    /// </example>
    [Pure]
    Action<TMemory, TStream> Compile<TMemory, TStream>(IBrainfProgram program)
        where TMemory : IBrainfMemory
        where TStream : IBrainfStream;
}