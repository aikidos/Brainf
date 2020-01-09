using Xunit;

namespace Brainf.Tests
{
    public sealed class BrainfMemoryTests
    {
        [Fact]
        public void Positive_Pointer()
        {
            // Arrange
            var memory = new BrainfMemory();
            var sum = 0;

            // Act
            for (var i = 0; i < 10; i++)
            {
                memory.CellValue = i;
                memory.Pointer = i;
            }

            for (var i = 9; i >= 0; i--)
            {
                memory.Pointer = i;
                sum += memory.CellValue;
            }

            // Assert
            Assert.Equal(45, sum);
        }
        
        [Fact]
        public void Negative_Pointer()
        {
            // Arrange
            var memory = new BrainfMemory();
            var sum = 0;

            // Act
            for (var i = -1; i >= -10; i--)
            {
                memory.CellValue = -(i + 1);
                memory.Pointer = i;
            }

            for (var i = -10; i <= -1 ; i++)
            {
                memory.Pointer = i;
                sum += memory.CellValue;
            }

            // Assert
            Assert.Equal(45, sum);
        }

        [Fact]
        public void Capacity()
        {
            // Arrange
            var memory = new BrainfMemory(10);

            // Act
            var capacity = memory.Capacity;

            for (var i = 0; i < 11; i++)
            {
                memory.CellValue = i;
                memory.Pointer++;
            }

            // Assert
            Assert.Equal(10, capacity);
            Assert.Equal(20, memory.Capacity);
        }
    }
}
