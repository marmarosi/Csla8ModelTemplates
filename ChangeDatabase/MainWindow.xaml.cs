using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;

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
            var database = comboBox.SelectedValue.ToString();

            var rootDir = Directory.GetCurrentDirectory();
            while (!File.Exists(Path.Combine(rootDir, "Csla8ModelTemplates.sln")))
            {
                rootDir = Directory.GetParent(rootDir).FullName;
            }

            var dataFile = Path.Combine(rootDir, "DockerData", $"{database}.txt");
            if (!File.Exists(dataFile))
                throw new Exception($"File DockerData\\{database}.txt dows not exist.");

            var dockerFile = Path.Combine(rootDir, "docker-compose.yml");
            var sourceLines = File.ReadLines(dockerFile);
            var targetLines = new StringBuilder();
            var isDatabaseLine = false;

            foreach ( var line in sourceLines )
            {

            }
        }
    }
}
