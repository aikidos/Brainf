﻿using Brainf.Streams;

namespace Brainf
{
    /// <summary>
    /// Contains reusable static instances of known implementations of the <see cref="IBrainfStream"/>.
    /// </summary>
    public static class KnownBrainfStreams
    {
        /// <summary>
        /// Implementation of the empty stream.
        /// </summary>
        public static EmptyBrainfStream Empty { get; } = new EmptyBrainfStream();

        /// <summary>
        /// Implementation of a write stream to a string.
        /// </summary>
        public static StringBrainfStream String { get; } = new StringBrainfStream();

        /// <summary>
        /// <see cref="System.Console"/> methods: <see cref="System.Console.Write(int)"/>, <see cref="System.Console.Read"/>.
        /// </summary>
        public static ConsoleBrainfStream Console { get; } = new ConsoleBrainfStream();
    }
}
