using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandBuilder
{
    class CommandParser
    {
        private static Regex commandPattern = new Regex(@"^(.*)?(\(\d+F?\))$", RegexOptions.IgnoreCase);
        private static Regex waitPattern = new Regex(@"\((\d+)(F?)\)", RegexOptions.IgnoreCase);

        public static List<Command> Parse(string commandText)
        {
            var lines = commandText.Replace("\r\n", "\n").Split('\n');
            var commandList = lines.SelectMany(command => CommandParser.ParseLine(command));
            return commandList.ToList();
        }

        public static List<Command> ParseLine(string line)
        {
            var commandList = new List<Command>();
            var commandKeyList = new List<CommandKey>();

            line = line.Trim();

            var commandSplit = commandPattern.Split(line).Where(str => str.Length > 0);
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
                    var chars = line.ToCharArray();
                    var inputs = new StringBuilder();

                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = chars[i];

                        if (Commands.GetAvailableChars().Contains(c))
                        {
                            inputs.Append(c);

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

            
            for(int i = 0; i < commandKeyList.Count(); i++)
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
                    else if(prevKey.IsAddSymbol() && !currentKey.IsAddSymbol())
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
    }
}
