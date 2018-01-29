using CommandBuilder.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommandBuilder
{
    class KeyInputReceiver
    {
        static Stopwatch stopwatch = new Stopwatch();
        static List<KeyFrame> keyBuffer = new List<KeyFrame>();
        static Dictionary<Key, bool[]> matrix = new Dictionary<Key, bool[]>();

        public static void Down(Key key)
        {
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
