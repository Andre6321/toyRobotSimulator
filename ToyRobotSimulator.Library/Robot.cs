namespace ToyRobotSimulator.Library
{
    using Dir = Direction;  // Alias to avoid name collision with property

    /// <summary>
    /// Represents the toy robot with its position and direction.
    /// </summary>
    public class Robot : IRobot
    {
        private Position? _position;
        private Direction? _direction;

        /// <summary>
        /// Gets the current position of the robot, or null if not placed.
        /// </summary>
        public Position? Position => _position;

        /// <summary>
        /// Gets the current direction the robot is facing, or null if not placed.
        /// </summary>
        public Direction? Direction => _direction;

        /// <summary>
        /// Indicates whether the robot has been placed on the table.
        /// </summary>
        public bool IsPlaced => _position != null && _direction != null;

        /// <summary>
        /// Places the robot at the specified position and direction.
        /// </summary>
        public void Place(Position position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        /// <summary>
        /// Moves the robot to a new position without changing direction.
        /// </summary>
        public void PlaceAt(Position position)
        {
            if (!IsPlaced)
                return;

            _position = position;
        }

        /// <summary>
        /// Rotates the robot 90 degrees to the left (counter-clockwise).
        /// </summary>
        public void TurnLeft()
        {
            if (!IsPlaced || _direction == null)
                return;

            _direction = _direction.Value switch
            {
                Dir.NORTH => Dir.WEST,
                Dir.WEST => Dir.SOUTH,
                Dir.SOUTH => Dir.EAST,
                Dir.EAST => Dir.NORTH,
                _ => _direction
            };
        }

        /// <summary>
        /// Rotates the robot 90 degrees to the right (clockwise).
        /// </summary>
        public void TurnRight()
        {
            if (!IsPlaced || _direction == null)
                return;

            _direction = _direction.Value switch
            {
                Dir.NORTH => Dir.EAST,
                Dir.EAST => Dir.SOUTH,
                Dir.SOUTH => Dir.WEST,
                Dir.WEST => Dir.NORTH,
                _ => _direction
            };
        }

        /// <summary>
        /// Calculates the next position if the robot moves forward one unit.
        /// </summary>
        /// <returns>The next position, or null if the robot is not placed</returns>
        public Position? GetNextPosition()
        {
            if (!IsPlaced || _position == null || _direction == null)
                return null;

            return _direction.Value switch
            {
                Dir.NORTH => new Position(_position.X, _position.Y + 1),
                Dir.EAST => new Position(_position.X + 1, _position.Y),
                Dir.SOUTH => new Position(_position.X, _position.Y - 1),
                Dir.WEST => new Position(_position.X - 1, _position.Y),
                _ => null
            };
        }

        /// <summary>
        /// Returns a report string with the robot's current position and direction.
        /// </summary>
        public string Report()
        {
            if (!IsPlaced || _position == null || _direction == null)
                return string.Empty;

            return $"{_position.X},{_position.Y},{_direction}";
        }
    }
}
