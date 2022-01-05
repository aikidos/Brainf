using Brainf.Compiler;
using Brainf.IO;
using Brainf.Memory;
using Brainf.Parser;
using Xunit;

namespace Brainf.Tests;

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
        var io = BrainfIO.CreateString();

        // Act
        var program = parser.Parse(sourceCode);
        var func = compiler.Compile(program);
        func(memory, io);

        // Assert
        Assert.Equal("ZYXWVUTSRQPONMLKJIHGFEDCBA", io.GetString());
    }
}