namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents a parsed command with its type and arguments.
    /// </summary>
    public record Command(
        CommandType Type,
        int? X = null,
        int? Y = null,
        Direction? Direction = null);

    /// <summary>
    /// The types of commands that can be executed.
    /// </summary>
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
