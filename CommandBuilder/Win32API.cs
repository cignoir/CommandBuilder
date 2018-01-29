using System;
using System.Runtime.InteropServices;

namespace CommandBuilder
{
    class Win32API
    {
        public const int MOD_ALT = 0x0001;
        public const int MOD_CONTROL = 0x0002;
        public const int MOD_SHIFT = 0x0004;
        public const int MOD_WIN = 0x0008;
        public const int WM_HOTKEY = 0x0312;
        public const int WM_HOTKEY_STOP = 0x0001;
        public const int WM_HOTKEY_START = 0x0002;
        public const int WM_HOTKEY1 = 0x0021;
        public const int WM_HOTKEY2 = 0x0022;
        public const int WM_HOTKEY3 = 0x0023;
        public const int WM_HOTKEY4 = 0x0024;
        public const int WM_HOTKEY5 = 0x0025;

        [DllImport("user32.dll")]
        public extern static int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);

        [DllImport("user32.dll")]
        public extern static int UnregisterHotKey(IntPtr HWnd, int ID);

        [DllImport("User32.Dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd,
            int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public const int HWND_TOP = 0;
        public const int HWND_BOTTOM = 1;
        public const int HWND_TOPMOST = -1;
        public const int HWND_NOTOPMOST = -2;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOW = 5;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_RESTORE = 9;
        public const int SW_FORCEMINIMIZE = 11;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(
            IntPtr hWnd, IntPtr ProcessId);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AttachThreadInput(
            uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo",
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfoGet(
            uint action, uint param, ref uint vparam, uint init);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo",
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfoSet(
            uint action, uint param, uint vparam, uint init);

        public const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
        public const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;
        public const uint SPIF_UPDATEINIFILE = 0x01;
        public const uint SPIF_SENDCHANGE = 0x02;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public const UInt32 WM_CLOSE = 0x0010;

        public const int WM_SYSCOMMAND = 274;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("User32.Dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

        [DllImport("User32.dll")]
        public static extern int MoveWindow(
            IntPtr hWnd,
            int x,
            int y,
            int nWidth,
            int nHeight,
            int bRepaint
            );
    }
}
