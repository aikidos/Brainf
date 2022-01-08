namespace Brainf.IO;

/// <summary>
/// Interface used for implementing input/output.
/// </summary>
public interface IBrainfIO
{
    /// <summary>
    /// Input a value.
    /// </summary>
    int Input();

    /// <summary>
    /// Output the specified value.
    /// </summary>
    /// <param name="value">Value.</param>
    void Output(int value);
}