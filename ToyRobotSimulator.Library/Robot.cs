namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents the toy robot with its position and direction.
    /// </summary>
    public class Robot
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

            _direction = _direction switch
            {
                Library.Direction.NORTH => Library.Direction.WEST,
                Library.Direction.WEST => Library.Direction.SOUTH,
                Library.Direction.SOUTH => Library.Direction.EAST,
                Library.Direction.EAST => Library.Direction.NORTH,
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

            _direction = _direction switch
            {
                Library.Direction.NORTH => Library.Direction.EAST,
                Library.Direction.EAST => Library.Direction.SOUTH,
                Library.Direction.SOUTH => Library.Direction.WEST,
                Library.Direction.WEST => Library.Direction.NORTH,
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

            return _direction switch
            {
                Library.Direction.NORTH => new Position(_position.X, _position.Y + 1),
                Library.Direction.EAST => new Position(_position.X + 1, _position.Y),
                Library.Direction.SOUTH => new Position(_position.X, _position.Y - 1),
                Library.Direction.WEST => new Position(_position.X - 1, _position.Y),
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
