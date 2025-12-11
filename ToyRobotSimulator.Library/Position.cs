namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents a position on the tabletop with X and Y coordinates.
    /// </summary>
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Checks if this position is within the valid bounds of the table.
        /// </summary>
        /// <param name="minX">Minimum X coordinate (inclusive)</param>
        /// <param name="minY">Minimum Y coordinate (inclusive)</param>
        /// <param name="maxX">Maximum X coordinate (inclusive)</param>
        /// <param name="maxY">Maximum Y coordinate (inclusive)</param>
        /// <returns>True if position is within bounds, false otherwise</returns>
        public bool IsWithinBounds(int minX, int minY, int maxX, int maxY)
        {
            return X >= minX && X <= maxX && Y >= minY && Y <= maxY;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   X == position.X &&
                   Y == position.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
