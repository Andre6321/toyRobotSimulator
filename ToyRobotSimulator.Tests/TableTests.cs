using ToyRobotSimulator.Library;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class TableTests
    {
        [Fact]
        public void Table_DefaultConstructor_Creates6x6Table()
        {
            // Arrange & Act
            var table = new Table();

            // Assert
            Assert.Equal(0, table.MinX);
            Assert.Equal(0, table.MinY);
            Assert.Equal(5, table.MaxX);
            Assert.Equal(5, table.MaxY);
        }

        [Fact]
        public void Table_CustomDimensions_SetsCorrectBounds()
        {
            // Arrange & Act
            var table = new Table(1, 2, 10, 20);

            // Assert
            Assert.Equal(1, table.MinX);
            Assert.Equal(2, table.MinY);
            Assert.Equal(10, table.MaxX);
            Assert.Equal(20, table.MaxY);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        [InlineData(3, 3)]
        public void IsValidPosition_InsideBounds_ReturnsTrue(int x, int y)
        {
            // Arrange
            var table = new Table();
            var position = new Position(x, y);

            // Act & Assert
            Assert.True(table.IsValidPosition(position));
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(6, 0)]
        [InlineData(0, 6)]
        public void IsValidPosition_OutsideBounds_ReturnsFalse(int x, int y)
        {
            // Arrange
            var table = new Table();
            var position = new Position(x, y);

            // Act & Assert
            Assert.False(table.IsValidPosition(position));
        }
    }
}
