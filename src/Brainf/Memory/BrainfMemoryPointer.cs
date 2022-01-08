using System;

namespace Brainf.Memory;

/// <summary>
/// Type that is used to represent a <see cref="BrainfMemory"/> pointer.
/// </summary>
internal readonly struct BrainfMemoryPointer
{
    /// <summary>
    /// Relative memory index.
    /// Always positive.
    /// </summary>
    public int RelativeIndex { get; }

    /// <summary>
    /// Indicates whether the negative index is used.
    /// </summary>
    public bool IsNegative { get; }

    /// <summary>
    /// Absolute memory index.
    /// Returns negative <see cref="RelativeIndex"/> if the value of <see cref="IsNegative"/> is `True`.
    /// Otherwise, positive.
    /// </summary>
    public int AbsoluteIndex => IsNegative ? -RelativeIndex : RelativeIndex;

    /// <summary>
    /// Initializes a new <see cref="BrainfMemoryPointer"/>.
    /// </summary>
    /// <param name="relativeIndex">Relative memory index.</param>
    /// <param name="isNegative">Indicates whether the negative index is used.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="relativeIndex"/> parameter is less than zero.
    /// </exception>
    public BrainfMemoryPointer(int relativeIndex, bool isNegative)
    {
        if (relativeIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(relativeIndex));

        RelativeIndex = relativeIndex;
        IsNegative = isNegative;
    }
}