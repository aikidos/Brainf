using System;

namespace Brainf.IO;

/// <summary>
/// Implementation of the console input/output.
/// </summary>
public sealed class ConsoleBrainfIO : IBrainfIO
{
    /// <inheritdoc />
    public int Input()
    {
        return Console.Read();
    }

    /// <inheritdoc />
    public void Output(int value)
    {
        Console.Write((char)value);
    }
}