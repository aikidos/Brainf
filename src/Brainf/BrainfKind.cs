namespace Brainf;

/// <summary>
/// `Brainfuck` operation kind.
/// </summary>
public enum BrainfKind
{
    /// <summary>
    /// `&gt;` — increment the data pointer (to point to the next cell to the right).
    /// </summary>
    MoveR,

    /// <summary>
    /// `&lt;` — decrement the data pointer (to point to the next cell to the left).
    /// </summary>
    MoveL,

    /// <summary>
    /// `+` — increment (increase by one) the byte at the data pointer.
    /// </summary>
    Inc,

    /// <summary>
    /// `-` — decrement (decrease by one) the byte at the data pointer.
    /// </summary>
    Dec,

    /// <summary>
    /// `.` — output the byte at the data pointer.
    /// </summary>
    Out,

    /// <summary>
    /// `,` — accept one byte of input, storing its value in the byte at the data pointer.
    /// </summary>
    In,
        
    /// <summary>
    /// `[` — if the byte at the data pointer is zero,
    ///       then instead of moving the instruction pointer forward to the next command,
    ///       jump it forward to the command after the matching ] command.
    /// </summary>
    LoopStart,
        
    /// <summary>
    /// `]` — if the byte at the data pointer is nonzero,
    ///       then instead of moving the instruction pointer forward to the next command,
    ///       jump it back to the command after the matching [ command.
    /// </summary>
    LoopEnd,
}