namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Interface for the tabletop.
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Gets the minimum X coordinate (inclusive).
        /// </summary>
        int MinX { get; }

        /// <summary>
        /// Gets the minimum Y coordinate (inclusive).
        /// </summary>
        int MinY { get; }

        /// <summary>
        /// Gets the maximum X coordinate (inclusive).
        /// </summary>
        int MaxX { get; }

        /// <summary>
        /// Gets the maximum Y coordinate (inclusive).
        /// </summary>
        int MaxY { get; }

        /// <summary>
        /// Checks if a position is valid (within the table bounds).
        /// </summary>
        /// <param name="position">The position to validate</param>
        /// <returns>True if the position is on the table, false otherwise</returns>
        bool IsValidPosition(Position position);
    }

    /// <summary>
    /// Interface for the robot.
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Gets the current position of the robot, or null if not placed.
        /// </summary>
        Position? Position { get; }

        /// <summary>
        /// Gets the current direction the robot is facing, or null if not placed.
        /// </summary>
        Direction? Direction { get; }

        /// <summary>
        /// Indicates whether the robot has been placed on the table.
        /// </summary>
        bool IsPlaced { get; }

        /// <summary>
        /// Places the robot at the specified position and direction.
        /// </summary>
        void Place(Position position, Direction direction);

        /// <summary>
        /// Moves the robot to a new position without changing direction.
        /// </summary>
        void PlaceAt(Position position);

        /// <summary>
        /// Rotates the robot 90 degrees to the left (counter-clockwise).
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Rotates the robot 90 degrees to the right (clockwise).
        /// </summary>
        void TurnRight();

        /// <summary>
        /// Calculates the next position if the robot moves forward one unit.
        /// </summary>
        Position? GetNextPosition();

        /// <summary>
        /// Returns a report string with the robot's current position and direction.
        /// </summary>
        string Report();
    }

    /// <summary>
    /// Interface for the command parser.
    /// </summary>
    public interface ICommandParser
    {
        /// <summary>
        /// Parses a command string into a Command object.
        /// </summary>
        /// <param name="commandString">The command string to parse</param>
        /// <returns>A Command object, or a Command with type INVALID if parsing fails</returns>
        Command Parse(string commandString);
    }

    /// <summary>
    /// Interface for the simulator.
    /// </summary>
    public interface ISimulator
    {
        /// <summary>
        /// Gets the robot being simulated.
        /// </summary>
        IRobot Robot { get; }

        /// <summary>
        /// Gets the table the robot is on.
        /// </summary>
        ITable Table { get; }

        /// <summary>
        /// Executes a command string and returns the result (if any).
        /// </summary>
        /// <param name="commandString">The command string to execute</param>
        /// <returns>The output of the command (e.g., REPORT output), or null if no output</returns>
        string? ExecuteCommand(string commandString);
    }
}
