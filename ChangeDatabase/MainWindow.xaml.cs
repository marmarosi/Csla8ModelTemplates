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
            var dataFile = Path.Combine(rootDir, "DockerData", $"{database}.txt");
            if (!File.Exists(dataFile))
            {
                message.Content = $"File DockerData\\{database}.txt dows not exist.";
                message.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }

            var dockerFile = Path.Combine(rootDir, "docker-compose.yml");
            var sourceLines = File.ReadLines(dockerFile);
            var targetLines = new StringBuilder();
            var isDatabasePart = false;

            foreach (var line in sourceLines)
            {
                if (line.Contains("# API Service"))
                    isDatabasePart = false;
                if (!isDatabasePart)
                    targetLines.AppendLine(line);
                if (line.Contains("# Database Service"))
                {
                    isDatabasePart = true;
                    targetLines.Append(File.ReadAllText(dataFile));
                }
            }
            using (StreamWriter writer = new StreamWriter(dockerFile, false))
            { 
                writer.Write(targetLines.ToString());
            }

            // Update app-settings.
            var settingsFile = Path.Combine(rootDir, "Csla8ModelTemplates.WebApi", "appsettings.json");
            sourceLines = File.ReadLines(settingsFile);
            targetLines = new StringBuilder();

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
