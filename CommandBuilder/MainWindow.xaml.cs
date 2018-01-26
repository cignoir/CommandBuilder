using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace CommandBuilder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            commandTextBox.AcceptsReturn = true;

            commandTextBox.Text = @"236+LP(30F)
LP(30F)
4+MP(10F)
(20F)
623+LK(100)";
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            var commands = CommandCache.FindOrBuild(commandTextBox.Text);
            stopwatch.Stop();

            long overhead = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            foreach (var command in commands)
            {
                command.Input();
                Console.WriteLine(command.ToString());
            }
            stopwatch.Stop();

            Console.WriteLine("\r\nOverhead: " + overhead + " ms");
            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine("===============================");
        }
    }
}
