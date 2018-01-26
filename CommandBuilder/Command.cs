using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace CommandBuilder
{
    class Command
    {
        private static IKeyboardSimulator keyboard = new InputSimulator().Keyboard;

        List<CommandKey> keys = new List<CommandKey>();

        public List<CommandKey> GetKeys()
        {
            return keys;
        }

        public void Add(CommandKey key)
        {
            keys.Add(key);
        }

        public Command With(CommandKey key)
        {
            keys.Add(key);
            return this;
        }

        override public string ToString()
        {
            return string.Join("+", keys.Select(k => k.ToString()));
        }

        public void Input()
        {
            foreach (var key in keys)
            {
                if (key.IsWait())
                {
                    Thread.Sleep(key.WaitMillis);
                }
                else
                {
                    var codes = key.GetVirtualKeyCodes();
                    foreach (var code in codes)
                    {
                        if (code != VirtualKeyCode.VK_5)
                        {
                            keyboard.KeyPress(code);
                        }
                    }
                }
            }

            foreach (var key in keys)
            {
                if (!key.IsWait())
                {
                    var codes = key.GetVirtualKeyCodes();
                    foreach (var code in codes)
                    {
                        if (code != VirtualKeyCode.VK_5)
                        {
                            keyboard.KeyUp(code);
                        }
                    }
                }
            }
        }
    }
}
