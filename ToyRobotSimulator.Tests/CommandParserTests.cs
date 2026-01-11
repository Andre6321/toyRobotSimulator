using ToyRobotSimulator.Library;
using Xunit;

namespace ToyRobotSimulator.Tests
{
    public class CommandParserTests
    {
        private readonly CommandParser _parser = new CommandParser();

        [Fact]
        public void TestParsePlaceCommandWithDirection()
        {
            // Act
            var command = _parser.Parse("PLACE 1,2,NORTH");

            // Assert
            Assert.Equal(CommandType.PLACE, command.Type);
            Assert.Equal(1, command.X);
            Assert.Equal(2, command.Y);
            Assert.Equal(Direction.NORTH, command.Direction);
        }

        [Fact]
        public void TestParsePlaceCommandWithoutDirection()
        {
            // Act
            var command = _parser.Parse("PLACE 3,4");

            // Assert
            Assert.Equal(CommandType.PLACE_AT, command.Type);
            Assert.Equal(3, command.X);
            Assert.Equal(4, command.Y);
            Assert.Null(command.Direction);
        }

        [Fact]
        public void TestParseMoveCommand()
        {
            // Act
            var command = _parser.Parse("MOVE");

            // Assert
            Assert.Equal(CommandType.MOVE, command.Type);
        }

        [Fact]
        public void TestParseLeftCommand()
        {
            // Act
            var command = _parser.Parse("LEFT");

            // Assert
            Assert.Equal(CommandType.LEFT, command.Type);
        }

        [Fact]
        public void TestParseRightCommand()
        {
            // Act
            var command = _parser.Parse("RIGHT");

            // Assert
            Assert.Equal(CommandType.RIGHT, command.Type);
        }

        [Fact]
        public void TestParseReportCommand()
        {
            // Act
            var command = _parser.Parse("REPORT");

            // Assert
            Assert.Equal(CommandType.REPORT, command.Type);
        }

        [Fact]
        public void TestParseInvalidCommand()
        {
            // Act
            var command = _parser.Parse("INVALID");

            // Assert
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParseEmptyString()
        {
            // Act
            var command = _parser.Parse("");

            // Assert
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParsePlaceWithInvalidCoordinates()
        {
            // Act
            var command = _parser.Parse("PLACE A,B,NORTH");

            // Assert
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParsePlaceWithInvalidDirection()
        {
            // Act
            var command = _parser.Parse("PLACE 1,2,NORTHEAST");

            // Assert
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParsePlaceWithMissingArguments()
        {
            // Act
            var command1 = _parser.Parse("PLACE");
            var command2 = _parser.Parse("PLACE 1");

            // Assert
            Assert.Equal(CommandType.INVALID, command1.Type);
            Assert.Equal(CommandType.INVALID, command2.Type);
        }

        [Fact]
        public void TestParseCaseInsensitiveCommands()
        {
            // Act
            var command1 = _parser.Parse("place 1,2,north");
            var command2 = _parser.Parse("Move");
            var command3 = _parser.Parse("LEFT");

            // Assert
            Assert.Equal(CommandType.PLACE, command1.Type);
            Assert.Equal(CommandType.MOVE, command2.Type);
            Assert.Equal(CommandType.LEFT, command3.Type);
        }

        [Fact]
        public void TestParseWithExtraWhitespace()
        {
            // Act
            var command = _parser.Parse("  PLACE   1,2,NORTH  ");

            // Assert
            Assert.Equal(CommandType.PLACE, command.Type);
            Assert.Equal(1, command.X);
            Assert.Equal(2, command.Y);
            Assert.Equal(Direction.NORTH, command.Direction);
        }

        [Fact]
        public void TestParseNullInput()
        {
            // Act
            var command = _parser.Parse(null!);

            // Assert
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParsePlaceWithExtraArguments()
        {
            // Act - PLACE with extra argument should still parse if first 3 are valid
            var command = _parser.Parse("PLACE 1,2,NORTH,EXTRA");

            // Assert - This should be invalid as it has too many args
            Assert.Equal(CommandType.INVALID, command.Type);
        }

        [Fact]
        public void TestParsePlaceWithNegativeCoordinates()
        {
            // Act
            var command = _parser.Parse("PLACE -1,-2,NORTH");

            // Assert - Parser should accept negative coords (boundary check is done elsewhere)
            Assert.Equal(CommandType.PLACE, command.Type);
            Assert.Equal(-1, command.X);
            Assert.Equal(-2, command.Y);
        }

        [Theory]
        [InlineData("PLACE 0,0,NORTH", CommandType.PLACE, 0, 0, Direction.NORTH)]
        [InlineData("PLACE 5,5,SOUTH", CommandType.PLACE, 5, 5, Direction.SOUTH)]
        [InlineData("PLACE 3,2,EAST", CommandType.PLACE, 3, 2, Direction.EAST)]
        [InlineData("PLACE 1,4,WEST", CommandType.PLACE, 1, 4, Direction.WEST)]
        public void Parse_ValidPlaceCommand_ReturnsCorrectCommand(
            string input, CommandType expectedType, int expectedX, int expectedY, Direction expectedDir)
        {
            // Act
            var command = _parser.Parse(input);

            // Assert
            Assert.Equal(expectedType, command.Type);
            Assert.Equal(expectedX, command.X);
            Assert.Equal(expectedY, command.Y);
            Assert.Equal(expectedDir, command.Direction);
        }

        [Theory]
        [InlineData("MOVE", CommandType.MOVE)]
        [InlineData("LEFT", CommandType.LEFT)]
        [InlineData("RIGHT", CommandType.RIGHT)]
        [InlineData("REPORT", CommandType.REPORT)]
        public void Parse_SimpleCommands_ReturnsCorrectType(string input, CommandType expectedType)
        {
            // Act
            var command = _parser.Parse(input);

            // Assert
            Assert.Equal(expectedType, command.Type);
        }
    }
}
