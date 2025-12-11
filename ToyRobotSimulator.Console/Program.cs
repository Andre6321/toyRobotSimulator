using ToyRobotSimulator.Library;

namespace ToyRobotSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================");
            Console.WriteLine("  Toy Robot Simulator");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  PLACE X,Y,DIRECTION - Place robot on table (e.g., PLACE 0,0,NORTH)");
            Console.WriteLine("  PLACE X,Y           - Move robot to new position (keeps current direction)");
            Console.WriteLine("  MOVE                - Move robot one unit forward");
            Console.WriteLine("  LEFT                - Rotate robot 90 degrees left");
            Console.WriteLine("  RIGHT               - Rotate robot 90 degrees right");
            Console.WriteLine("  REPORT              - Display robot's position and direction");
            Console.WriteLine("  EXIT                - Exit the simulator");
            Console.WriteLine();
            Console.WriteLine("Table dimensions: 6x6 (0,0 to 5,5)");
            Console.WriteLine("Directions: NORTH, SOUTH, EAST, WEST");
            Console.WriteLine("==================================");
            Console.WriteLine();

            var simulator = new Simulator();
            bool running = true;

            while (running)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                input = input.Trim();

                if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    running = false;
                    Console.WriteLine("Goodbye!");
                    continue;
                }

                var result = simulator.ExecuteCommand(input);

                if (result != null)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
