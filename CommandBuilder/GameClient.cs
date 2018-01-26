using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandBuilder
{
    class GameClient
    {
        Process proc;
        IntPtr hWnd;

        public GameClient(Process proc)
        {
            this.proc = proc;
            this.hWnd = proc.MainWindowHandle;
        }

        public void Activate()
        {
            if (hWnd == IntPtr.Zero)
            {
                return;
            }

            if (Win32API.IsIconic(hWnd))
            {
                //SendMessage(hWnd, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
                //ShowWindowAsync(hWnd, SW_RESTORE);
                //SetFocus(hWnd);
                Win32API.SwitchToThisWindow(hWnd, true);
            }

            IntPtr forehWnd = Win32API.GetForegroundWindow();
            if (forehWnd == hWnd)
            {
                return;
            }
            uint foreThread = Win32API.GetWindowThreadProcessId(forehWnd, IntPtr.Zero);
            uint thisThread = Win32API.GetCurrentThreadId();
            uint timeout = 200000;
            if (foreThread != thisThread)
            {
                Win32API.SystemParametersInfoGet(Win32API.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, ref timeout, 0);
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, 0, 0);

                Win32API.AttachThreadInput(thisThread, foreThread, true);
            }

            Win32API.SetForegroundWindow(hWnd);
            Win32API.SetWindowPos(hWnd, Win32API.HWND_TOP, 0, 0, 0, 0,
                Win32API.SWP_NOMOVE | Win32API.SWP_NOSIZE | Win32API.SWP_SHOWWINDOW | Win32API.SWP_ASYNCWINDOWPOS);
            Win32API.BringWindowToTop(hWnd);
            //ShowWindowAsync(hWnd, SW_SHOW);
            Win32API.SetFocus(hWnd);
            Win32API.SwitchToThisWindow(hWnd, true);

            if (foreThread != thisThread)
            {
                Win32API.SystemParametersInfoSet(Win32API.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, timeout, 0);
                Win32API.AttachThreadInput(thisThread, foreThread, false);
            }
        }
    }
}
