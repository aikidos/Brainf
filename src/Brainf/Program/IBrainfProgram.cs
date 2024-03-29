﻿using System;
using System.Diagnostics.Contracts;
using Brainf.Parser;

namespace Brainf.Program;

/// <summary>
/// Interface used for implementing `Brainfuck` program.
/// </summary>
public interface IBrainfProgram
{
    /// <summary>
    /// Gets the source code.
    /// </summary>
    string SourceCode { get; }
        
    /// <summary>
    /// Returns all program operations.
    /// </summary>
    /// <seealso cref="IBrainfParser.Parse"/>
    /// <seealso cref="BrainfOperation.Kind"/>
    /// <example>
    /// <code>
    ///     const string sourceCode = ...;
    ///  
    ///     var parser = new BrainfParser();
    ///  
    ///     var program = parser.Parse(sourceCode);
    ///  
    ///     var operations = program.GetOperations();
    ///  
    ///     for (int i = 0; i &lt; operations.Length; i++)
    ///     {
    ///         Console.WriteLine(operations[i].Kind);
    ///     } 
    /// </code>
    /// </example>
    [Pure]
    ReadOnlySpan<BrainfOperation> GetOperations();
}