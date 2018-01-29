using System.Collections.Generic;
using System.Linq;

namespace CommandBuilder
{
    class CommandCache
    {
        public static Dictionary<int, List<Command>> Storage = new Dictionary<int, List<Command>>();

        public static List<Command> FindOrBuild(string commandText)
        {
            var hashCode = commandText.GetHashCode();

            if (!Storage.Keys.ToList().Contains(hashCode))
            {
                Storage.Add(hashCode, CommandParser.Parse(commandText));
            }

            return Storage[hashCode];
        }
    }
}
