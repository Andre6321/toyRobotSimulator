namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Represents the tabletop with its dimensions and boundary validation.
    /// </summary>
    public class Table : ITable
    {
        /// <summary>
        /// Gets the minimum X coordinate (inclusive).
        /// </summary>
        public int MinX { get; }

        /// <summary>
        /// Gets the minimum Y coordinate (inclusive).
        /// </summary>
        public int MinY { get; }

        /// <summary>
        /// Gets the maximum X coordinate (inclusive).
        /// </summary>
        public int MaxX { get; }

        /// <summary>
        /// Gets the maximum Y coordinate (inclusive).
        /// </summary>
        public int MaxY { get; }

        /// <summary>
        /// Creates a new table with the specified dimensions.
        /// </summary>
        /// <param name="minX">Minimum X coordinate (default: 0)</param>
        /// <param name="minY">Minimum Y coordinate (default: 0)</param>
        /// <param name="maxX">Maximum X coordinate (default: 5)</param>
        /// <param name="maxY">Maximum Y coordinate (default: 5)</param>
        public Table(int minX = 0, int minY = 0, int maxX = 5, int maxY = 5)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        /// <summary>
        /// Checks if a position is valid (within the table bounds).
        /// </summary>
        /// <param name="position">The position to validate</param>
        /// <returns>True if the position is on the table, false otherwise</returns>
        public bool IsValidPosition(Position position) =>
            position.IsWithinBounds(MinX, MinY, MaxX, MaxY);
    }
}
