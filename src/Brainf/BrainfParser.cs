using System;
using System.Collections.Generic;

namespace Brainf
{
    /// <summary>
    /// Implementation of the `Brainfuck` parser. 
    /// </summary>
    public sealed class BrainfParser : IBrainfParser
    {
        /// <summary>
        /// Default implementation the `Brainfuck` parser.
        /// </summary>
        public static IBrainfParser Default { get; } = new BrainfParser();
        
        /// <inheritdoc />
        public IBrainfProgram Parse(string sourceCode)
        {
            if (sourceCode == null) 
                throw new ArgumentNullException(nameof(sourceCode));
            
            if (string.IsNullOrWhiteSpace(sourceCode))
                return new BrainfProgram(sourceCode, Array.Empty<BrainfOperation>());

            var operations = new List<BrainfOperation>();
            BrainfKind? lastKind = null;
            
            int count = 1;

            var span = sourceCode.AsSpan();

            for (int i = 0; i < span.Length; i++)
            {
                var kind = span[i] switch
                {
                    '>' => BrainfKind.MoveR,
                    '<' => BrainfKind.MoveL,
                    '+' => BrainfKind.Inc,
                    '-' => BrainfKind.Dec,
                    '.' => BrainfKind.Out,
                    ',' => BrainfKind.In,
                    '[' => BrainfKind.LoopStart,
                    ']' => BrainfKind.LoopEnd,
                    _ => default(BrainfKind?)
                };

                if (kind == null)
                    continue;

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

            return new BrainfProgram(sourceCode, operations.ToArray());
        }
    }
}
