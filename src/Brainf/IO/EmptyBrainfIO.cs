namespace Brainf.IO;

/// <summary>
/// Implementation of the empty input/output.
/// </summary>
public sealed class EmptyBrainfIO : IBrainfIO
{
    /// <inheritdoc />
    public int Input()
    {
        return 0;
    }

    /// <inheritdoc />
    public void Output(int value)
    { }
}