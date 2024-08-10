using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChangeDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBox_Click(object sender, RoutedEventArgs e)
        {
            message.Content = "";
            message.Foreground = new SolidColorBrush(Colors.Gray);

            var cbi = (ComboBoxItem)comboBox.SelectedValue;
            var database = cbi.Content.ToString();

            var rootDir = Directory.GetCurrentDirectory();
            while (!File.Exists(Path.Combine(rootDir, "Csla8ModelTemplates.sln")))
            {
                rootDir = Directory.GetParent(rootDir).FullName;
            }

            // Update docker-compose.
            var setupFile = Path.Combine(rootDir, "Setup", $"{database}.docker-compose.yml");
            if (!File.Exists(setupFile))
            {
                message.Content = $"File Setup\\{database}.docker-compose.yml does not exist.";
                message.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }
            var setupContent = File.ReadAllText(setupFile);

            var dockerFile = Path.Combine(rootDir, "docker-compose.yml");
            using (StreamWriter writer = new StreamWriter(dockerFile, false))
            { 
                writer.Write(setupContent);
            }

            // Update app-settings files.
            var projectList = new string[] { "Csla8ModelTemplates.WebApi", "Csla8ModelTemplates.Tests.WebApi" };
            foreach (var project in projectList)
            {
                var settingsFile = Path.Combine(rootDir, project, "AppSettings.json");
                var sourceLines = File.ReadLines(settingsFile);
                var targetLines = new StringBuilder();

                foreach (var line in sourceLines)
                {
                    if (line.Contains("ActiveDals"))
                        targetLines.AppendLine($"  \"ActiveDals\": [ \"{database}\" ],");
                    else
                        targetLines.AppendLine(line);
                }
                using (StreamWriter writer = new StreamWriter(settingsFile, false))
                {
                    writer.Write(targetLines.ToString());
                }
            }

            // Done.
            message.Content = "Done.";
            message.Foreground = new SolidColorBrush(Colors.Green);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (message != null)
            {
                message.Content = "";
                message.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
