using System.Text;

namespace Brainf.Streams
{
    /// <summary>
    /// Implementation of a write stream to a string. 
    /// </summary>
    public sealed class StringBrainfStream : IBrainfStream
    {
        private readonly StringBuilder _builder = new StringBuilder();

        /// <summary>
        /// Returns the resulting string.
        /// </summary>
        public string GetString()
        {
            return _builder.ToString();
        }

        /// <inheritdoc />
        public void Write(int value)
        {
            _builder.Append((char) value);
        }

        /// <inheritdoc />
        public int Read()
        {
            throw new System.NotImplementedException();
        }
    }
}
