namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Parses command strings into Command objects.
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// Parses a command string into a Command object.
        /// </summary>
        /// <param name="commandString">The command string to parse</param>
        /// <returns>A Command object, or a Command with type INVALID if parsing fails</returns>
        public Command Parse(string commandString)
        {
            if (string.IsNullOrWhiteSpace(commandString))
                return new Command(CommandType.INVALID);

            var parts = commandString.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return new Command(CommandType.INVALID);

            var commandName = parts[0].ToUpper();

            return commandName switch
            {
                "PLACE" => ParsePlaceCommand(parts),
                "MOVE" => new Command(CommandType.MOVE),
                "LEFT" => new Command(CommandType.LEFT),
                "RIGHT" => new Command(CommandType.RIGHT),
                "REPORT" => new Command(CommandType.REPORT),
                _ => new Command(CommandType.INVALID)
            };
        }

        private Command ParsePlaceCommand(string[] parts)
        {
            if (parts.Length < 2)
                return new Command(CommandType.INVALID);

            var args = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            if (args.Length < 2)
                return new Command(CommandType.INVALID);

            // Parse X and Y coordinates
            if (!int.TryParse(args[0].Trim(), out int x) || 
                !int.TryParse(args[1].Trim(), out int y))
            {
                return new Command(CommandType.INVALID);
            }

            // Check if direction is provided
            if (args.Length == 3)
            {
                // Full PLACE command with direction
                if (Enum.TryParse<Direction>(args[2].Trim().ToUpper(), out Direction direction))
                {
                    return new Command(CommandType.PLACE, x, y, direction);
                }
                return new Command(CommandType.INVALID);
            }
            else if (args.Length == 2)
            {
                // PLACE command without direction (move to coordinates)
                return new Command(CommandType.PLACE_AT, x, y);
            }

            return new Command(CommandType.INVALID);
        }
    }
}
