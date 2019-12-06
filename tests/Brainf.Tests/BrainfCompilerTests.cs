using Brainf.Streams;
using Xunit;

namespace Brainf.Tests
{
    public sealed class BrainfCompilerTests
    {
        [Fact]
        public void Compile()
        {
            // Arrange
            const string sourceCode = @"
>++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
[>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
++++++++[>++++++++++[>++++++++++[>++++++++++[>+
+++++++++[-]<-]<-]<-]<-]<-]<-]<-]";
            
            var parser = new BrainfParser();
            var compiler = new BrainfCompiler();

            var memory = new BrainfMemory();
            var stream = new StringBrainfStream();

            // Act
            var program = parser.Parse(sourceCode);
            var func = compiler.Compile<BrainfMemory, StringBrainfStream>(program);
            func(memory, stream);
            
            // Assert
            Assert.Equal("ZYXWVUTSRQPONMLKJIHGFEDCBA", stream.GetString());
        }
    }
}