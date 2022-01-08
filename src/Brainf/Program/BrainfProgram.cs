using System;
using System.Collections.Generic;
using System.Linq;

namespace Brainf.Program;

/// <summary>
/// `Brainfuck` program.
/// </summary>
public sealed class BrainfProgram : IBrainfProgram
{
    private readonly BrainfOperation[] _operations;

    /// <inheritdoc />
    public string SourceCode { get; }

    /// <summary>
    /// Initializes a new <see cref="BrainfProgram"/>.
    /// </summary>
    /// <param name="sourceCode">The source code.</param>
    /// <param name="operations">Program operations.</param>
    /// <exception cref="ArgumentNullException">
    ///     The <paramref name="sourceCode"/> parameter is null.
    ///     The <paramref name="operations"/> parameter is null.
    /// </exception>
    internal BrainfProgram(string sourceCode, IEnumerable<BrainfOperation> operations)
    {
        SourceCode = sourceCode;
        _operations = operations.ToArray();
    }

    /// <inheritdoc />
    public ReadOnlySpan<BrainfOperation> GetOperations()
    {
        return _operations;
    }
}