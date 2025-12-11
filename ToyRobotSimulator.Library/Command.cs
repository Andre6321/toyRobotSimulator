namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents a parsed command with its type and arguments.
    /// </summary>
    public class Command
    {
        public CommandType Type { get; }
        public int? X { get; }
        public int? Y { get; }
        public Direction? Direction { get; }

        public Command(CommandType type, int? x = null, int? y = null, Direction? direction = null)
        {
            Type = type;
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    public enum CommandType
    {
        PLACE,
        PLACE_AT,  // PLACE command without direction
        MOVE,
        LEFT,
        RIGHT,
        REPORT,
        INVALID
    }
}
