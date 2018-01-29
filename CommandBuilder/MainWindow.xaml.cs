using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CommandBuilder
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        GameClient gameClient;

        public MainWindow()
        {
            InitializeComponent();
            commandTextBox.AcceptsReturn = true;

            commandTextBox.Text = @"236+LP(30F)
LP(30F)
4+MP(10F)
(20F)
623+LK(100)";

            DetectGameClient();
        }

        private void DetectGameClient()
        {
            var proccesses = Process.GetProcessesByName(GameClient.processName);
            if (proccesses != null && proccesses.Length > 0)
            {
                this.gameClient = new GameClient(proccesses.First());
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.gameClient == null)
            {
                DetectGameClient();
            }

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Restart();
            var commands = CommandCache.FindOrBuild(commandTextBox.Text);
            stopwatch.Stop();

            long overhead = stopwatch.ElapsedMilliseconds;

            if (gameClient != null)
            {
                gameClient.Activate();

                stopwatch.Restart();
                foreach (var command in commands)
                {
                    command.Input();
                    Console.WriteLine(command.ToString());
                }
                stopwatch.Stop();
            }

            Console.WriteLine("\r\nOverhead: " + overhead + " ms");
            Console.WriteLine("Elapsed: " + stopwatch.ElapsedMilliseconds + " ms");
            Console.WriteLine("===============================");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInputReceiver.Down(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            KeyInputReceiver.Up(e.Key);
        }
    }
}
