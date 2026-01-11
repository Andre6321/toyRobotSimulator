namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents a position on the tabletop with X and Y coordinates.
    /// </summary>
    public record Position(int X, int Y)
    {
        /// <summary>
        /// Checks if this position is within the valid bounds of the table.
        /// </summary>
        /// <param name="minX">Minimum X coordinate (inclusive)</param>
        /// <param name="minY">Minimum Y coordinate (inclusive)</param>
        /// <param name="maxX">Maximum X coordinate (inclusive)</param>
        /// <param name="maxY">Maximum Y coordinate (inclusive)</param>
        /// <returns>True if position is within bounds, false otherwise</returns>
        public bool IsWithinBounds(int minX, int minY, int maxX, int maxY) =>
            X >= minX && X <= maxX && Y >= minY && Y <= maxY;

        public override string ToString() => $"{X},{Y}";
    }
}
