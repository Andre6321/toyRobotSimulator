using ToyRobotSimulator.Library;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class PositionTests
    {
        [Fact]
        public void TestPositionCreation()
        {
            // Arrange & Act
            var position = new Position(3, 4);

            // Assert
            Assert.Equal(3, position.X);
            Assert.Equal(4, position.Y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, 3)]
        [InlineData(5, 5)]
        [InlineData(2, 4)]
        public void IsWithinBounds_InsideBounds_ReturnsTrue(int x, int y)
        {
            // Arrange
            var position = new Position(x, y);

            // Act
            var result = position.IsWithinBounds(0, 0, 5, 5);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(6, 0)]
        [InlineData(0, 6)]
        [InlineData(-1, -1)]
        [InlineData(6, 6)]
        public void IsWithinBounds_OutsideBounds_ReturnsFalse(int x, int y)
        {
            // Arrange
            var position = new Position(x, y);

            // Act & Assert
            Assert.False(position.IsWithinBounds(0, 0, 5, 5));
        }
    }
}
