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
    ///     var func = compiler.Compile(program);
    ///
    ///     var memory = new BrainfMemory();
    ///
    ///     func(memory, BrainfStreams.Console);
    /// </code>
    /// </example>
    [Pure]
    Action<IBrainfMemory, IBrainfStream> Compile(IBrainfProgram program);
}