using System;

namespace Brainf.Streams
{
    /// <summary>
    /// Implementation of the console stream. 
    /// </summary>
    public sealed class ConsoleBrainfStream : IBrainfStream
    {
        /// <inheritdoc />
        public void Write(int value)
        {
            Console.Write((char) value);
        }

        /// <inheritdoc />
        public int Read()
        {
            return Console.Read();
        }
    }
}
