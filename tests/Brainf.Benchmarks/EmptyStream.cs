namespace Brainf.Benchmarks
{
    public sealed class EmptyStream : IBrainfStream
    {
        public void Write(int value)
        { }

        public int Read()
        {
            return 0;
        }
    }
}
