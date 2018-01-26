using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace CommandBuilder
{
    class CommandKey
    {
        public string Code { get; set; }
        public int WaitFrame { get; set; }
        public int WaitMillis { get; set; }

        public CommandKey(string code)
        {
            this.Code = code;
        }

        public bool IsAddSymbol()
        {
            return Code.Equals("+");
        }

        public bool IsWait()
        {
            return Code.Equals("WT");
        }

        public void SetWaitFrame(int frame)
        {
            this.WaitFrame = frame;
            this.WaitMillis = Commands.FrameToMillis(frame);
        }

        public void SetWaitMillis(int millis)
        {
            this.WaitFrame = Commands.MillisToFrame(millis);
            this.WaitMillis = millis;
        }

        public override string ToString()
        {
            return IsWait() ? string.Format("({0}F,{1}ms)", WaitFrame, WaitMillis) : Code;
        }

        public List<VirtualKeyCode> GetVirtualKeyCodes()
        {
            var codes = new List<VirtualKeyCode>();

            switch(Code)
            {
                case "1":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_A, VirtualKeyCode.VK_S };
                    break;
                case "2":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_S };
                    break;
                case "3":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_D, VirtualKeyCode.VK_S };
                    break;
                case "4":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_A };
                    break;
                case "6":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_D };
                    break;
                case "7":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_A, VirtualKeyCode.VK_W };
                    break;
                case "8":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_W };
                    break;
                case "9":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_D, VirtualKeyCode.VK_W };
                    break;
                case "DS":
                    break;
                case "BS":
                    break;
                case "J":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_W };
                    break;
                case "LP":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_G };
                    break;
                case "MP":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_H };
                    break;
                case "HP":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_J };
                    break;
                case "LK":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_B };
                    break;
                case "MK":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_N };
                    break;
                case "HK":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_M };
                    break;
                case "PP":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_G, VirtualKeyCode.VK_H };
                    break;
                case "PPP":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J };
                    break;
                case "KK":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_B, VirtualKeyCode.VK_N };
                    break;
                case "KKK":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M };
                    break;
                case "VS":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_H, VirtualKeyCode.VK_N };
                    break;
                case "VT":
                    new List<VirtualKeyCode>() { VirtualKeyCode.VK_J, VirtualKeyCode.VK_M };
                    break;
                default:
                    break;
            }

            return codes;
        }
    }
}
