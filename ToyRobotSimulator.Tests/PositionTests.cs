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

        [Fact]
        public void TestIsWithinBounds_InsideBounds()
        {
            // Arrange
            var position = new Position(3, 3);

            // Act
            var result = position.IsWithinBounds(0, 0, 5, 5);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TestIsWithinBounds_OnBoundary()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(5, 5);

            // Act & Assert
            Assert.True(position1.IsWithinBounds(0, 0, 5, 5));
            Assert.True(position2.IsWithinBounds(0, 0, 5, 5));
        }

        [Fact]
        public void TestIsWithinBounds_OutsideBounds()
        {
            // Arrange
            var position1 = new Position(-1, 0);
            var position2 = new Position(0, -1);
            var position3 = new Position(6, 0);
            var position4 = new Position(0, 6);

            // Act & Assert
            Assert.False(position1.IsWithinBounds(0, 0, 5, 5));
            Assert.False(position2.IsWithinBounds(0, 0, 5, 5));
            Assert.False(position3.IsWithinBounds(0, 0, 5, 5));
            Assert.False(position4.IsWithinBounds(0, 0, 5, 5));
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var position = new Position(2, 3);

            // Act
            var result = position.ToString();

            // Assert
            Assert.Equal("2,3", result);
        }

        [Fact]
        public void TestEquality()
        {
            // Arrange
            var position1 = new Position(2, 3);
            var position2 = new Position(2, 3);
            var position3 = new Position(3, 2);

            // Act & Assert
            Assert.True(position1.Equals(position2));
            Assert.False(position1.Equals(position3));
        }

        [Fact]
        public void TestGetHashCode()
        {
            // Arrange
            var position1 = new Position(2, 3);
            var position2 = new Position(2, 3);

            // Act & Assert
            Assert.Equal(position1.GetHashCode(), position2.GetHashCode());
        }
    }
}
