using System.Collections.Generic;
using System.Linq;

namespace CommandBuilder
{
    class Commands
    {
        public static CommandKey DEFAULT = new CommandKey("5");
        public static CommandKey D1 = new CommandKey("1");
        public static CommandKey D2 = new CommandKey("2");
        public static CommandKey D3 = new CommandKey("3");
        public static CommandKey D4 = new CommandKey("4");
        public static CommandKey D5 = new CommandKey("5");
        public static CommandKey D6 = new CommandKey("6");
        public static CommandKey D7 = new CommandKey("7");
        public static CommandKey D8 = new CommandKey("8");
        public static CommandKey D9 = new CommandKey("9");

        public static CommandKey DS = new CommandKey("DS");
        public static CommandKey BS = new CommandKey("BS");
        public static CommandKey J = new CommandKey("J");

        public static CommandKey LP = new CommandKey("LP");
        public static CommandKey MP = new CommandKey("MP");
        public static CommandKey HP = new CommandKey("HP");
        public static CommandKey LK = new CommandKey("LK");
        public static CommandKey MK = new CommandKey("MK");
        public static CommandKey HK = new CommandKey("HK");

        public static CommandKey PP = new CommandKey("PP");
        public static CommandKey KK = new CommandKey("KK");

        public static CommandKey WT = new CommandKey("WT");

        public static CommandKey VT = new CommandKey("VT");
        public static CommandKey VS = new CommandKey("VS");

        public static CommandKey PLUS = new CommandKey("+");

        public static List<CommandKey> GetDefinedKeys()
        {
            return new List<CommandKey>() { DEFAULT, D1, D2, D3, D4, D5, D6, D7, D8, D9, DS, BS, J, LP, MP, HP, LK, MK, HK, PP, KK, WT, VT, VS, PLUS };
        }

        public static List<string> GetDefinedKeyCodes()
        {
            return GetDefinedKeys().Select(k => k.Code).Distinct().ToList();
        }

        public static List<char> GetAvailableChars()
        {
            return new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'J', 'L', 'M', 'H', 'P', 'K', 'W', 'T', 'V', 'S', '+', '(', ')' };
        }

        public static CommandKey Find(string chars)
        {
            var matchKeys = GetDefinedKeys().Where(k => k.Code.Equals(chars));
            return matchKeys != null && matchKeys.Count() > 0 ? matchKeys.First() : DEFAULT;
        }
    }
}
