using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace CommandBuilder
{
    class KeyInputReceiver
    {
        static Stopwatch debugStopwatch = new Stopwatch();
        static Stopwatch stopwatch = new Stopwatch();
        static List<KeyFrame> keyBuffer = new List<KeyFrame>();
        public static Dictionary<Key, bool[]> matrix = new Dictionary<Key, bool[]>();

        public static void Down(Key key)
        {
            //debugStopwatch.Restart();
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                keyBuffer.Clear();
                matrix = new Dictionary<Key, bool[]>();
            }

            if (keyBuffer.Count() == 0)
            {
                stopwatch.Restart();
                var keyFrame = new KeyFrame(key, KeyStatus.Down, stopwatch.ElapsedMilliseconds);
                keyBuffer.Add(keyFrame);
                matrix = CommandParser.CreateMatrix(keyBuffer, matrix, key);
            }
            else
            {
                var lastKey = keyBuffer.Last();
                if (!(lastKey.key.ToString().Equals(key.ToString()) && lastKey.status == KeyStatus.Down))
                {
                    var keyFrame = new KeyFrame(key, KeyStatus.Down, stopwatch.ElapsedMilliseconds);
                    keyBuffer.Add(keyFrame);
                    matrix = CommandParser.CreateMatrix(keyBuffer, matrix, key);
                }
            }
            //debugStopwatch.Stop();
            //Console.WriteLine(debugStopwatch.ElapsedMilliseconds);
        }

        public static void Up(Key key)
        {
            if (keyBuffer.Exists(kb => kb.key == key && kb.status == KeyStatus.Down))
            {
                var keyFrame = new KeyFrame(key, KeyStatus.Up, stopwatch.ElapsedMilliseconds);
                keyBuffer.Add(keyFrame);
            }
        }
    }
}
