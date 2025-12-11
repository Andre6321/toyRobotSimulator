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

        [Fact]
        public void TestGetNextPositionNorth()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(2, 2), Direction.NORTH);

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.NotNull(nextPosition);
            Assert.Equal(2, nextPosition?.X);
            Assert.Equal(3, nextPosition?.Y);
        }

        [Fact]
        public void TestGetNextPositionEast()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(2, 2), Direction.EAST);

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.NotNull(nextPosition);
            Assert.Equal(3, nextPosition?.X);
            Assert.Equal(2, nextPosition?.Y);
        }

        [Fact]
        public void TestGetNextPositionSouth()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(2, 2), Direction.SOUTH);

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.NotNull(nextPosition);
            Assert.Equal(2, nextPosition?.X);
            Assert.Equal(1, nextPosition?.Y);
        }

        [Fact]
        public void TestGetNextPositionWest()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(2, 2), Direction.WEST);

            // Act
            var nextPosition = robot.GetNextPosition();

            // Assert
            Assert.NotNull(nextPosition);
            Assert.Equal(1, nextPosition?.X);
            Assert.Equal(2, nextPosition?.Y);
        }

        [Fact]
        public void TestTurnLeftFromNorth()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);

            // Act
            robot.TurnLeft();

            // Assert
            Assert.Equal(Direction.WEST, robot.Direction);
        }

        [Fact]
        public void TestTurnRightFromNorth()
        {
            // Arrange
            var robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);

            // Act
            robot.TurnRight();

            // Assert
            Assert.Equal(Direction.EAST, robot.Direction);
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
    }
}
