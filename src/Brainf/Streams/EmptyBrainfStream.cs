namespace Brainf.Streams;

/// <summary>
/// Implementation of the empty stream.
/// </summary>
public sealed class EmptyBrainfStream : IBrainfStream
{
    /// <inheritdoc />
    public void Write(int value)
    { }

    /// <inheritdoc />
    public int Read()
    {
        return 0;
    }
}