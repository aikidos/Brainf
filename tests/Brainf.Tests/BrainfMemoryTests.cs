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
            int sum = 0;

            // Act
            for (int i = 0; i < 10; i++)
            {
                memory.Pointer = i;
                memory.CellValue = i;
            }

            for (int i = 9; i >= 0; i--)
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
            int sum = 0;

            // Act
            for (int i = -1; i >= -10; i--)
            {
                memory.Pointer = i;
                memory.CellValue = -(i + 1);
            }

            for (int i = -10; i <= -1 ; i++)
            {
                memory.Pointer = i;
                sum += memory.CellValue;
            }

            // Assert
            Assert.Equal(45, sum);
        }
    }
}
