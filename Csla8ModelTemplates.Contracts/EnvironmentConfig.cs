namespace Csla8ModelTemplates.Contracts
{
    /// <summary>
    /// Provides helper methods to setup environment variables.
    /// </summary>
    public sealed class EnvironmentConfig
    {
        private readonly Dictionary<string, string> _data = new Dictionary<string, string>();

        /// <summary>
        /// Creates a new intsance.
        /// </summary>
        /// <param name="path">The path of the environment configuration file.</param>
        public EnvironmentConfig(
            string path
            )
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var values = line.Split(':');
                var name = values[0].Trim();
                var value = values[1].Trim();
                _data.Add($"{name}.name", $"{name.ToUpper()}_CONNSTR");
                _data.Add($"{name}.value", $"{value}");
            }
        }

        /// <summary>
        /// Gets the name of the environment variable for the specified DAL.
        /// </summary>
        /// <param name="database">The name of the DAL.</param>
        /// <returns>The name of the environment variable.</returns>
        public string GetName(
            string database
            )
        {
            return _data[$"{database}.name"];
        }

        /// <summary>
        /// Gets the value of the environment variable for the specified DAL.
        /// </summary>
        /// <param name="database">The name of the DAL.</param>
        /// <returns>The value of the environment variable.</returns>
        public string GetValue(
            string database
            )
        {
            return _data[$"{database}.value"];
        }
    }
}
