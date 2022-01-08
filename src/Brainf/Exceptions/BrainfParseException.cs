using System;

namespace Brainf.Exceptions;

/// <summary>
/// The exception that is thrown when a syntax error is found in an programs source code.
/// </summary>
public sealed class BrainfParseException : Exception
{
    /// <summary>
    /// Initializes a new <see cref="BrainfParseException"/>.
    /// </summary>
    /// <param name="message">Syntax error message.</param>
    internal BrainfParseException(string message) 
        : base(message)
    { }
}