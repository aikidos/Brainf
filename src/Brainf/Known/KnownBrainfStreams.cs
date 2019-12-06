namespace Brainf
{
    /// <summary>
    /// Contains reusable static instances of known implementations of the <see cref="IBrainfStream"/>.
    /// </summary>
    public static class KnownBrainfStreams
    {
        /// <summary>
        /// <see cref="System.Console"/> methods: <see cref="System.Console.Write(int)"/>, <see cref="System.Console.Read"/>.
        /// </summary>
        public static IBrainfStream Console { get; } = new ConsoleBrainfStream();
    }
}
