using ToyRobotSimulator.Library;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class RobotTests
    {
        [Fact]
        public void TestRobotInitiallyNotPlaced()
        {
            // Arrange
            var robot = new Robot();

            // Assert
            Assert.False(robot.IsPlaced);
            Assert.Null(robot.Position);
            Assert.Null(robot.Direction);
        }

        [Fact]
        public void TestPlaceRobotOnTable()
        {
            // Arrange
            var robot = new Robot();
            var position = new Position(1, 2);

            // Act
            robot.Place(position, Direction.NORTH);

            // Assert
            Assert.True(robot.IsPlaced);
            Assert.Equal(1, robot.Position?.X);
            Assert.Equal(2, robot.Position?.Y);
            Assert.Equal(Direction.NORTH, robot.Direction);
        }

        [Fact]
        public void TestRobotReport()
        {
            // Arrange
            var robot = new Robot();
            var position = new Position(3, 4);

            // Act
            robot.Place(position, Direction.EAST);
            var report = robot.Report();

            // Assert
            Assert.Equal("3,4,EAST", report);
        }

        [Fact]
        public void TestRobotReportWhenNotPlaced()
        {
            // Arrange
            var robot = new Robot();

            // Act
            var report = robot.Report();

            // Assert
            Assert.Equal(string.Empty, report);
        }

        [Theory]
        [InlineData(Direction.NORTH, Direction.WEST)]
        [InlineData(Direction.WEST, Direction.SOUTH)]
        [InlineData(Direction.SOUTH, Direction.EAST)]
        [InlineData(Direction.EAST, Direction.NORTH)]
        public void TurnLeft_ShouldRotateCounterClockwise(Direction start, Direction expected)
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(0, 0), start);

            // Act
            robot.TurnLeft();

            // Assert
            Assert.Equal(expected, robot.Direction);
        }

        [Theory]
        [InlineData(Direction.NORTH, Direction.EAST)]
        [InlineData(Direction.EAST, Direction.SOUTH)]
        [InlineData(Direction.SOUTH, Direction.WEST)]
        [InlineData(Direction.WEST, Direction.NORTH)]
        public void TurnRight_ShouldRotateClockwise(Direction start, Direction expected)
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(0, 0), start);

            // Act
            robot.TurnRight();

            // Assert
            Assert.Equal(expected, robot.Direction);
        }

        [Theory]
        [InlineData(Direction.NORTH, 2, 3)]
        [InlineData(Direction.EAST, 3, 2)]
        [InlineData(Direction.SOUTH, 2, 1)]
        [InlineData(Direction.WEST, 1, 2)]
        public void GetNextPosition_ShouldCalculateCorrectPosition(Direction direction, int expectedX, int expectedY)
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(2, 2), direction);

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.NotNull(nextPosition);
            Assert.Equal(expectedX, nextPosition?.X);
            Assert.Equal(expectedY, nextPosition?.Y);
        }

        [Fact]
        public void TestPlaceAtMovesRobotWithoutChangingDirection()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);

            // Act
            robot.PlaceAt(new Position(3, 4));

            // Assert
            Assert.Equal(3, robot.Position?.X);
            Assert.Equal(4, robot.Position?.Y);
            Assert.Equal(Direction.NORTH, robot.Direction);
        }

        [Fact]
        public void TestPlaceAtIgnoredWhenNotPlaced()
        {
            // Arrange
            var robot = new Robot();

            // Act
            robot.PlaceAt(new Position(3, 4));

            // Assert
            Assert.False(robot.IsPlaced);
        }

        [Fact]
        public void TurnLeft_WhenNotPlaced_ShouldDoNothing()
        {
            // Arrange
            var robot = new Robot();

            // Act
            robot.TurnLeft();

            // Assert
            Assert.False(robot.IsPlaced);
            Assert.Null(robot.Direction);
        }

        [Fact]
        public void TurnRight_WhenNotPlaced_ShouldDoNothing()
        {
            // Arrange
            var robot = new Robot();

            // Act
            robot.TurnRight();

            // Assert
            Assert.False(robot.IsPlaced);
            Assert.Null(robot.Direction);
        }

        [Fact]
        public void GetNextPosition_WhenNotPlaced_ShouldReturnNull()
        {
            // Arrange
            var robot = new Robot();

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.Null(nextPosition);
        }
    }
}
