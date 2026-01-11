namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Simulates the toy robot on a tabletop.
    /// </summary>
    public class Simulator : ISimulator
    {
        private readonly IRobot _robot;
        private readonly ICommandParser _parser;
        private readonly ITable _table;

        /// <summary>
        /// Creates a new simulator with the specified table dimensions.
        /// </summary>
        /// <param name="minX">Minimum X coordinate (default: 0)</param>
        /// <param name="minY">Minimum Y coordinate (default: 0)</param>
        /// <param name="maxX">Maximum X coordinate (default: 5)</param>
        /// <param name="maxY">Maximum Y coordinate (default: 5)</param>
        public Simulator(int minX = 0, int minY = 0, int maxX = 5, int maxY = 5)
            : this(new Robot(), new CommandParser(), new Table(minX, minY, maxX, maxY))
        {
        }

        /// <summary>
        /// Creates a new simulator with injectable dependencies (for testing).
        /// </summary>
        public Simulator(IRobot robot, ICommandParser parser, ITable table)
        {
            _robot = robot;
            _parser = parser;
            _table = table;
        }

        /// <summary>
        /// Gets the robot being simulated.
        /// </summary>
        public IRobot Robot => _robot;

        /// <summary>
        /// Gets the table the robot is on.
        /// </summary>
        public ITable Table => _table;

        /// <summary>
        /// Executes a command string and returns the result (if any).
        /// </summary>
        /// <param name="commandString">The command string to execute</param>
        /// <returns>The output of the command (e.g., REPORT output), or null if no output</returns>
        public string? ExecuteCommand(string commandString)
        {
            var command = _parser.Parse(commandString);

            if (command.Type == CommandType.INVALID)
                return null;

            return command.Type switch
            {
                CommandType.PLACE => ExecutePlace(command),
                CommandType.PLACE_AT => ExecutePlaceAt(command),
                CommandType.MOVE => ExecuteMove(),
                CommandType.LEFT => ExecuteLeft(),
                CommandType.RIGHT => ExecuteRight(),
                CommandType.REPORT => ExecuteReport(),
                _ => null
            };
        }

        private string? ExecutePlace(Command command)
        {
            if (command.X == null || command.Y == null || command.Direction == null)
                return null;

            var position = new Position(command.X.Value, command.Y.Value);
            
            // Validate position is within bounds
            if (!_table.IsValidPosition(position))
                return null;

            _robot.Place(position, command.Direction.Value);
            return null;
        }

        private string? ExecutePlaceAt(Command command)
        {
            // Robot must already be placed for PLACE_AT to work
            if (!_robot.IsPlaced)
                return null;

            if (command.X == null || command.Y == null)
                return null;

            var position = new Position(command.X.Value, command.Y.Value);
            
            // Validate position is within bounds
            if (!_table.IsValidPosition(position))
                return null;

            _robot.PlaceAt(position);
            return null;
        }

        private string? ExecuteMove()
        {
            if (!_robot.IsPlaced)
                return null;

            var nextPosition = _robot.GetNextPosition();
            if (nextPosition == null)
                return null;

            // Only move if the next position is within bounds
            if (_table.IsValidPosition(nextPosition))
            {
                _robot.PlaceAt(nextPosition);
            }

            return null;
        }

        private string? ExecuteLeft()
        {
            if (!_robot.IsPlaced)
                return null;

            _robot.TurnLeft();
            return null;
        }

        private string? ExecuteRight()
        {
            if (!_robot.IsPlaced)
                return null;

            _robot.TurnRight();
            return null;
        }

        private string? ExecuteReport()
        {
            if (!_robot.IsPlaced)
                return null;

            return _robot.Report();
        }
    }
}
