using ToyRobotSimulator.Library;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class SimulatorTests
    {
        [Fact]
        public void TestExample_A()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            simulator.ExecuteCommand("MOVE");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,1,NORTH", result);
        }

        [Fact]
        public void TestExample_B()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            simulator.ExecuteCommand("LEFT");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,0,WEST", result);
        }

        [Fact]
        public void TestExample_C()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 1,2,EAST");
            simulator.ExecuteCommand("MOVE");
            simulator.ExecuteCommand("MOVE");
            simulator.ExecuteCommand("LEFT");
            simulator.ExecuteCommand("MOVE");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("3,3,NORTH", result);
        }

        [Fact]
        public void TestExample_D()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 1,2,EAST");
            simulator.ExecuteCommand("MOVE");
            simulator.ExecuteCommand("LEFT");
            simulator.ExecuteCommand("MOVE");
            simulator.ExecuteCommand("PLACE 3,1");
            simulator.ExecuteCommand("MOVE");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("3,2,NORTH", result);
        }

        [Fact]
        public void TestRobotIgnoresCommandsBeforeValidPlace()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("MOVE");
            simulator.ExecuteCommand("LEFT");
            simulator.ExecuteCommand("RIGHT");
            var reportBeforePlace = simulator.ExecuteCommand("REPORT");
            
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            var reportAfterPlace = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Null(reportBeforePlace);
            Assert.Equal("0,0,NORTH", reportAfterPlace);
        }

        [Fact]
        public void TestRobotPreventsMovementOffTable_North()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,5,NORTH");
            simulator.ExecuteCommand("MOVE"); // Should not move off table
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,5,NORTH", result);
        }

        [Fact]
        public void TestRobotPreventsMovementOffTable_South()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,SOUTH");
            simulator.ExecuteCommand("MOVE"); // Should not move off table
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,0,SOUTH", result);
        }

        [Fact]
        public void TestRobotPreventsMovementOffTable_East()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 5,0,EAST");
            simulator.ExecuteCommand("MOVE"); // Should not move off table
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("5,0,EAST", result);
        }

        [Fact]
        public void TestRobotPreventsMovementOffTable_West()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,WEST");
            simulator.ExecuteCommand("MOVE"); // Should not move off table
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,0,WEST", result);
        }

        [Fact]
        public void TestInvalidPlaceCommandsAreIgnored()
        {
            // Arrange
            var simulator = new Simulator();

            // Act - Try invalid placements
            simulator.ExecuteCommand("PLACE 6,0,NORTH"); // X out of bounds
            simulator.ExecuteCommand("PLACE 0,6,NORTH"); // Y out of bounds
            simulator.ExecuteCommand("PLACE -1,0,NORTH"); // Negative X
            simulator.ExecuteCommand("PLACE 0,-1,NORTH"); // Negative Y
            var reportBeforeValid = simulator.ExecuteCommand("REPORT");

            simulator.ExecuteCommand("PLACE 2,2,NORTH"); // Valid placement
            var reportAfterValid = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Null(reportBeforeValid);
            Assert.Equal("2,2,NORTH", reportAfterValid);
        }

        [Fact]
        public void TestRightTurnRotatesClockwise()
        {
            // Arrange
            var simulator = new Simulator();

            // Act & Assert
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            Assert.Equal("0,0,NORTH", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("RIGHT");
            Assert.Equal("0,0,EAST", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("RIGHT");
            Assert.Equal("0,0,SOUTH", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("RIGHT");
            Assert.Equal("0,0,WEST", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("RIGHT");
            Assert.Equal("0,0,NORTH", simulator.ExecuteCommand("REPORT"));
        }

        [Fact]
        public void TestLeftTurnRotatesCounterClockwise()
        {
            // Arrange
            var simulator = new Simulator();

            // Act & Assert
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            Assert.Equal("0,0,NORTH", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("LEFT");
            Assert.Equal("0,0,WEST", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("LEFT");
            Assert.Equal("0,0,SOUTH", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("LEFT");
            Assert.Equal("0,0,EAST", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("LEFT");
            Assert.Equal("0,0,NORTH", simulator.ExecuteCommand("REPORT"));
        }

        [Fact]
        public void TestMultiplePlaceCommands()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            Assert.Equal("0,0,NORTH", simulator.ExecuteCommand("REPORT"));

            simulator.ExecuteCommand("PLACE 3,3,SOUTH");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("3,3,SOUTH", result);
        }

        [Fact]
        public void TestPlaceAtCommandMovesWithoutChangingDirection()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            simulator.ExecuteCommand("PLACE 3,4"); // Move without changing direction
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("3,4,NORTH", result);
        }

        [Fact]
        public void TestPlaceAtIsIgnoredWhenRobotNotPlaced()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 3,4"); // Should be ignored
            var reportBeforePlace = simulator.ExecuteCommand("REPORT");

            simulator.ExecuteCommand("PLACE 1,1,EAST");
            var reportAfterPlace = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Null(reportBeforePlace);
            Assert.Equal("1,1,EAST", reportAfterPlace);
        }

        [Fact]
        public void TestInvalidCommandsAreIgnored()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("PLACE 0,0,NORTH");
            simulator.ExecuteCommand("INVALID");
            simulator.ExecuteCommand("JUMP");
            simulator.ExecuteCommand("");
            var result = simulator.ExecuteCommand("REPORT");

            // Assert
            Assert.Equal("0,0,NORTH", result);
        }

        [Fact]
        public void TestCaseSensitivityOfCommands()
        {
            // Arrange
            var simulator = new Simulator();

            // Act
            simulator.ExecuteCommand("place 0,0,north");
            simulator.ExecuteCommand("move");
            simulator.ExecuteCommand("left");
            var result = simulator.ExecuteCommand("report");

            // Assert
            Assert.Equal("0,1,WEST", result);
        }
    }
}
