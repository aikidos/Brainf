using System;
using System.Diagnostics;

namespace Brainf.Program;

/// <summary>
/// `Brainfuck` program operation.
/// </summary>
[DebuggerDisplay("{" + nameof(Kind) + "}")]
public readonly struct BrainfOperation
{
    /// <summary>
    /// Gets the operation kind.
    /// </summary>
    public BrainfKind Kind { get; }
        
    /// <summary>
    /// Gets the number of repetitions of the operation.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Initializes a new <see cref="BrainfOperation"/>.
    /// </summary>
    /// <param name="kind">Operation kind.</param>
    /// <param name="count">Program operations.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="count"/> parameter is less than or equal to zero.
    /// </exception>
    public BrainfOperation(BrainfKind kind, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        Kind = kind;
        Count = count;
    }
}