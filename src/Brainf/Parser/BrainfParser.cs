using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Brainf.Exceptions;
using Brainf.Program;

namespace Brainf.Parser;

/// <summary>
/// Implementation of the `Brainfuck` parser.
/// </summary>
public sealed class BrainfParser : IBrainfParser
{
    /// <inheritdoc />
    public IBrainfProgram Parse(string sourceCode)
    {
        if (sourceCode == null)
            throw new ArgumentNullException(nameof(sourceCode));

        return TryParse(sourceCode, out var program, out var errorMessage)
            ? program
            : throw new BrainfParseException(errorMessage);
    }

    /// <inheritdoc />
    public bool TryParse(
        string sourceCode,
        [NotNullWhen(true)] out IBrainfProgram? program,
        [NotNullWhen(false)] out string? errorMessage)
    {
        if (sourceCode == null)
            throw new ArgumentNullException(nameof(sourceCode));

        if (string.IsNullOrWhiteSpace(sourceCode))
        {
            program = new BrainfProgram(string.Empty, Enumerable.Empty<BrainfOperation>());
            errorMessage = null;
            return true;
        }

        var operations = new List<BrainfOperation>();
        BrainfKind? lastKind = null;

        var count = 1;
        var openedLoops = 0;

        var span = sourceCode.AsSpan();

        for (var i = 0; i < span.Length; i++)
        {
            BrainfKind? kind = null;

            switch (span[i])
            {
                case '>':
                    kind = BrainfKind.MoveR;
                    break;

                case '<':
                    kind = BrainfKind.MoveL;
                    break;

                case '+':
                    kind = BrainfKind.Inc;
                    break;

                case '-':
                    kind = BrainfKind.Dec;
                    break;

                case '.':
                    kind = BrainfKind.Out;
                    break;

                case ',':
                    kind = BrainfKind.In;
                    break;

                case '[':
                    openedLoops++;
                    kind = BrainfKind.LoopStart;
                    break;

                case ']':
                    if (--openedLoops < 0)
                    {
                        program = null;
                        errorMessage = "Invalid `]`. There was no opening token - `[`.";
                        return false;
                    }

                    kind = BrainfKind.LoopEnd;
                    break;
            }

            if (kind == null)
            {
                continue;
            }

            if (lastKind != kind)
            {
                if (lastKind.HasValue)
                {
                    operations.Add(new BrainfOperation(lastKind.Value, count));
                }

                lastKind = kind;

                count = 1;
            }
            else
            {
                count++;
            }
        }

        if (lastKind != null)
        {
            operations.Add(new BrainfOperation(lastKind.Value, count));
        }

        if (openedLoops > 0)
        {
            program = null;
            errorMessage = "Invalid `[`. There was no closing token - `]`.";
            return false;
        }

        program = new BrainfProgram(sourceCode, operations);
        errorMessage = null;
        return true;
    }
}