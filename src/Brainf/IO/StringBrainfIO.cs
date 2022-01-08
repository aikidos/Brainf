using System.Text;

namespace Brainf.IO;

/// <summary>
/// Implementation of input/output to a string.
/// </summary>
public sealed class StringBrainfIO : IBrainfIO
{
    private readonly StringBuilder _builder = new();

    /// <summary>
    /// Returns the resulting string.
    /// </summary>
    public string GetString()
    {
        return _builder.ToString();
    }

    /// <inheritdoc />
    public int Input()
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public void Output(int value)
    {
        _builder.Append((char)value);
    }
}