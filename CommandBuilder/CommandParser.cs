using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CommandBuilder
{
    class CommandParser
    {
        private static Regex splitPattern = new Regex(@"(\(\d+F?\))", RegexOptions.IgnoreCase);
        private static Regex waitPattern = new Regex(@"\((\d+)(F?)\)", RegexOptions.IgnoreCase);

        public static List<Command> Parse(string commandText)
        {
            var lines = commandText.Replace("\r\n", "\n").Split('\n');
            var commandList = lines.SelectMany(commandLine => CommandParser.ParseLine(commandLine));
            return commandList.ToList();
        }

        public static List<Command> ParseLine(string line)
        {
            var keys = CreateCommandKeys(line);
            var commands = CreateCommands(keys);
            return commands;
        }

        public static List<CommandKey> CreateCommandKeys(string line)
        {
            var commandKeyList = new List<CommandKey>();
            var commandSplit = splitPattern.Split(line.Trim()).Where(str => str.Length > 0);
            foreach (var s in commandSplit)
            {
                var m = waitPattern.Match(s);
                if (m.Success)
                {
                    var waitKey = new CommandKey("WT");
                    int waitNum = int.Parse(m.Groups[1].Value);
                    string degree = m.Groups[2].Value.ToUpper();

                    if (degree.Equals("F"))
                    {
                        waitKey.SetWaitFrame(waitNum);
                    }
                    else
                    {
                        waitKey.SetWaitMillis(waitNum);
                    }
                    commandKeyList.Add(waitKey);
                }
                else
                {
                    var chars = s.ToCharArray();
                    var inputs = new StringBuilder();

                    for (int i = 0; i < s.Length; i++)
                    {
                        if (Commands.GetAvailableChars().Contains(chars[i]))
                        {
                            inputs.Append(chars[i]);

                            var chain = inputs.ToString();
                            if (Commands.GetDefinedKeyCodes().Contains(chain))
                            {
                                commandKeyList.Add(Commands.Find(chain));
                                inputs.Clear();
                            }
                        }
                    }
                }
            }
            return commandKeyList;
        }

        public static List<Command> CreateCommands(List<CommandKey> commandKeyList)
        {
            var commandList = new List<Command>();

            for (int i = 0; i < commandKeyList.Count(); i++)
            {
                if (i == 0)
                {
                    var command = new Command().With(commandKeyList[i]);
                    commandList.Add(command);
                }
                else
                {
                    var prevKey = commandKeyList[i - 1];
                    var currentKey = commandKeyList[i];

                    if (prevKey.IsAddSymbol() && currentKey.IsAddSymbol())
                    {
                        continue;
                    }
                    else if (prevKey.IsAddSymbol() && !currentKey.IsAddSymbol())
                    {
                        commandList.Last().Add(currentKey);
                    }
                    else if (!prevKey.IsAddSymbol() && currentKey.IsAddSymbol())
                    {
                        continue;
                    }
                    else
                    {
                        commandList.Add(new Command().With(currentKey));
                    }
                }
            }

            return commandList;
        }

        class Segment {
            public KeyFrame Start { set; get; }
            public KeyFrame End { set; get; }
            public Segment(KeyFrame start, KeyFrame end)
            {
                Start = start;
                End = end;
            }
        }

        public static Dictionary<Key, bool[]> CreateMatrix(List<KeyFrame> keyFrames, Dictionary<Key, bool[]> dict, Key targetKey)
        {
            var groups = keyFrames.GroupBy(kf => kf.key);
            foreach (var group in groups)
            {
                // TODO:
                if (targetKey != group.Key)
                {
                    continue;
                }

                var key = group.Key;
                bool[] matrix = new bool[(int)keyFrames.Last().frameCount + 1];

                KeyFrame start = group.First();

                var segments = new List<Segment>();
                foreach (var item in group)
                {
                    if (item.status == KeyStatus.Down)
                    {
                        if (start == null)
                        {
                            start = item;
                        }
                    }
                    else
                    {
                        if (start != null)
                        {
                            segments.Add(new Segment(start, item));
                            start = null;
                        }
                    }
                }

                if (start != null)
                {
                    segments.Add(new Segment(start, null));
                }

                foreach(Segment segment in segments)
                {
                    int startFrameCount = (int)segment.Start.frameCount;

                    int endFrameCount;
                    if (segment.End == null)
                    {
                        endFrameCount = (int)keyFrames.Last().frameCount;
                    }
                    else
                    {
                        endFrameCount = (int)segment.End.frameCount;
                    }

                    for (int i = startFrameCount; i <= endFrameCount; i++)
                    {
                        matrix[i] = true;
                    }
                }

                dict[key] = matrix;
            }

            //Console.WriteLine("-------------------------");
            //foreach (var key in dict.Keys)
            //{   
            //    var dump = string.Join(",", dict[key].Select(item => item ? "1" : "0"));
            //    Console.WriteLine(key.ToString() + ": " + dump);
            //}

            return dict;
        }
    }
}
