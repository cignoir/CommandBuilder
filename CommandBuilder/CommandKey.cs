﻿using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace CommandBuilder
{
    class CommandKey
    {
        public string Code { get; set; }
        public float WaitFrame { get; set; }
        public long WaitMillis { get; set; }

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

        public bool IsMoveKey()
        {
            List<string> numerics = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            return numerics.Contains(Code);
        }

        public void SetWaitFrame(float frame)
        {
            this.WaitFrame = frame;
            this.WaitMillis = CommandUtils.FrameToMillis(frame);
        }

        public void SetWaitMillis(long millis)
        {
            this.WaitFrame = CommandUtils.MillisToFrame(millis);
            this.WaitMillis = millis;
        }

        public override string ToString()
        {
            var code = "";
            if (IsWait())
            {
                if (WaitFrame <= 2.0f)
                {
                    code = "+";
                }
                else
                {
                    code = string.Format("({0}F)", (int)Math.Round(WaitFrame));
                }
            }
            else
            {
                code = Code;
            }
            return code;
        }

        public List<VirtualKeyCode> GetVirtualKeyCodes()
        {
            var codes = new List<VirtualKeyCode>();

            switch(Code)
            {
                case "1":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_A, VirtualKeyCode.VK_S };
                    break;
                case "2":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_S };
                    break;
                case "3":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_D, VirtualKeyCode.VK_S };
                    break;
                case "4":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_A };
                    break;
                case "6":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_D };
                    break;
                case "7":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_A, VirtualKeyCode.VK_W };
                    break;
                case "8":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_W };
                    break;
                case "9":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_D, VirtualKeyCode.VK_W };
                    break;
                case "DS":
                    break;
                case "BS":
                    break;
                case "J":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_W };
                    break;
                case "LP":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_G };
                    break;
                case "MP":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_H };
                    break;
                case "HP":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_J };
                    break;
                case "LK":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_B };
                    break;
                case "MK":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_N };
                    break;
                case "HK":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_M };
                    break;
                case "PP":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_G, VirtualKeyCode.VK_H };
                    break;
                case "PPP":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_G, VirtualKeyCode.VK_H, VirtualKeyCode.VK_J };
                    break;
                case "KK":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_B, VirtualKeyCode.VK_N };
                    break;
                case "KKK":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_B, VirtualKeyCode.VK_N, VirtualKeyCode.VK_M };
                    break;
                case "VS":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_H, VirtualKeyCode.VK_N };
                    break;
                case "VT":
                    codes = new List<VirtualKeyCode>() { VirtualKeyCode.VK_J, VirtualKeyCode.VK_M };
                    break;
                default:
                    break;
            }

            return codes;
        }

        public override bool Equals(object obj)
        {
            return this.Code.Equals(((CommandKey)obj).Code);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
