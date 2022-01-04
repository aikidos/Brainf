namespace Brainf.Streams;

/// <summary>
/// Interface used for implementing stream.
/// </summary>
public interface IBrainfStream
{
    /// <summary>
    /// Writes the specified value.
    /// </summary>
    /// <param name="value">Value.</param>
    void Write(int value);

    /// <summary>
    /// Reads a value.
    /// </summary>
    int Read();
}