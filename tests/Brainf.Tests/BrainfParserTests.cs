using Brainf.Parser;
using Brainf.Program;
using Xunit;

namespace Brainf.Tests;

public sealed class BrainfParserTests
{
    [Fact]
    public void Parse()
    {
        // Arrange
        const string sourceCode = "+++--->>><<<...,,,[[]]";
            
        var parser = new BrainfParser();

        // Act
        var program = parser.Parse(sourceCode);
        var operations = program.GetOperations();

        // Assert
        Assert.Equal(sourceCode, program.SourceCode);
        Assert.Equal(8, operations.Length);
            
        Assert.Equal(BrainfKind.Inc, operations[0].Kind);
        Assert.Equal(3, operations[0].Count);
            
        Assert.Equal(BrainfKind.Dec, operations[1].Kind);
        Assert.Equal(3, operations[1].Count);
            
        Assert.Equal(BrainfKind.MoveR, operations[2].Kind);
        Assert.Equal(3, operations[2].Count);
            
        Assert.Equal(BrainfKind.MoveL, operations[3].Kind);
        Assert.Equal(3, operations[3].Count);
            
        Assert.Equal(BrainfKind.Out, operations[4].Kind);
        Assert.Equal(3, operations[4].Count);
            
        Assert.Equal(BrainfKind.In, operations[5].Kind);
        Assert.Equal(3, operations[5].Count);
            
        Assert.Equal(BrainfKind.LoopStart, operations[6].Kind);
        Assert.Equal(2, operations[6].Count);
            
        Assert.Equal(BrainfKind.LoopEnd, operations[7].Kind);
        Assert.Equal(2, operations[7].Count);
    }

    [Theory]
    [InlineData("[]", true)]
    [InlineData("[", false)]
    [InlineData("]", false)]
    public void TryParse(string sourceCode, bool isValid)
    {
        // Arrange
        var parser = new BrainfParser();

        // Act
        var result = parser.TryParse(sourceCode, out var program, out var errorMessage);

        // Assert
        Assert.Equal(isValid, result);

        if (isValid)
        {
            Assert.NotNull(program);
            Assert.Null(errorMessage);
        }
        else
        {
            Assert.Null(program);
            Assert.NotNull(errorMessage);
        }
    }
}