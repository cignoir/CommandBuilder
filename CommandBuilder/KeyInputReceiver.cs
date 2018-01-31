using System;
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
        static List<CommandKey> keyBuffer = new List<CommandKey>();

        public static string Down(CommandKey key)
        {
            var elapsedTime = stopwatch.ElapsedMilliseconds;

            //debugStopwatch.Restart();
            if (elapsedTime > 500)
            {
                keyBuffer.Clear();
            }

            if (keyBuffer.Count() == 0)
            {
                keyBuffer.Add(key);
            }
            else
            {
                var waitKey = new CommandKey("WT");
                waitKey.SetWaitMillis(elapsedTime);

                if ((int)waitKey.WaitFrame <= 1)
                {
                    var lastKey = keyBuffer.Last();

                    if (lastKey.IsMoveKey() && key.IsMoveKey())
                    {
                        if (Commands.CanMerge(lastKey, key))
                        {
                            keyBuffer.RemoveAt(keyBuffer.Count() - 1);
                            keyBuffer.Add(Commands.Merge(lastKey, key));
                        }
                        else
                        {
                            keyBuffer.Add(key);
                        }
                    }
                    else
                    {
                        //keyBuffer.Add(Commands.PLUS);
                        keyBuffer.Add(waitKey);
                        keyBuffer.Add(key);
                    }                    
                }
                else
                {
                    keyBuffer.Add(waitKey);
                    keyBuffer.Add(key);
                }
            }

            var log = string.Join("", keyBuffer);
            stopwatch.Restart();

            //Console.WriteLine(string.Join("", keyBuffer));

            //debugStopwatch.Stop();
            //Console.WriteLine(debugStopwatch.ElapsedMilliseconds);
            return log;
        }
    }
}
