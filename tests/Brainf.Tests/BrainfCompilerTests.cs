using System.Collections.Generic;
using Moq;
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

            var outputValues = new List<char>();
            
            var brainfStream = new Mock<IBrainfStream>();
            brainfStream.Setup(stream => stream.Write(It.IsAny<int>()))
                .Callback((int value) => outputValues.Add((char) value));

            // Act
            var program = parser.Parse(sourceCode);
            var func = compiler.Compile(program);
            func(brainfStream.Object);
            
            // Assert
            Assert.Equal("ZYXWVUTSRQPONMLKJIHGFEDCBA", new string(outputValues.ToArray()));
        }
    }
}