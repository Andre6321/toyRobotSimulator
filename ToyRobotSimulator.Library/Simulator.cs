namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Simulates the toy robot on a tabletop.
    /// </summary>
    public class Simulator
    {
        private readonly Robot _robot;
        private readonly CommandParser _parser;
        private readonly int _minX;
        private readonly int _minY;
        private readonly int _maxX;
        private readonly int _maxY;

        /// <summary>
        /// Creates a new simulator with the specified table dimensions.
        /// </summary>
        /// <param name="minX">Minimum X coordinate (default: 0)</param>
        /// <param name="minY">Minimum Y coordinate (default: 0)</param>
        /// <param name="maxX">Maximum X coordinate (default: 5)</param>
        /// <param name="maxY">Maximum Y coordinate (default: 5)</param>
        public Simulator(int minX = 0, int minY = 0, int maxX = 5, int maxY = 5)
        {
            _robot = new Robot();
            _parser = new CommandParser();
            _minX = minX;
            _minY = minY;
            _maxX = maxX;
            _maxY = maxY;
        }

        /// <summary>
        /// Gets the robot being simulated.
        /// </summary>
        public Robot Robot => _robot;

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
            if (!position.IsWithinBounds(_minX, _minY, _maxX, _maxY))
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
            if (!position.IsWithinBounds(_minX, _minY, _maxX, _maxY))
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
            if (nextPosition.IsWithinBounds(_minX, _minY, _maxX, _maxY))
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
