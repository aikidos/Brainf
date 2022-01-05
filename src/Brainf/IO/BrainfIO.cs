namespace Brainf.IO;

/// <summary>
/// Contains reusable static instances of known implementations of the <see cref="IBrainfIO"/>.
/// </summary>
public static class BrainfIO
{
    /// <summary>
    /// Implementation of the empty input/output.
    /// </summary>
    public static EmptyBrainfIO Empty { get; } = new();

    /// <summary>
    /// Implementation of the console input/output.
    /// </summary>
    public static ConsoleBrainfIO Console { get; } = new();

    /// <summary>
    /// Returns new instance of the implementation input/output to a string.
    /// </summary>
    public static StringBrainfIO CreateString() => new();
}