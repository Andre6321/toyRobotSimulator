namespace ToyRobotSimulator.Library
{
    /// <summary>
    /// Extension methods for testing the Simulator.
    /// </summary>
    public static class SimulatorExtensions
    {
        /// <summary>
        /// Executes multiple commands in sequence.
        /// </summary>
        /// <param name="simulator">The simulator instance</param>
        /// <param name="commands">The commands to execute</param>
        /// <returns>The simulator for method chaining</returns>
        public static Simulator Execute(this Simulator simulator, params string[] commands)
        {
            foreach (var command in commands)
            {
                simulator.ExecuteCommand(command);
            }
            return simulator;
        }
    }
}
