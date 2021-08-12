using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop; // HwndHost
using System.Runtime.InteropServices; // DllImport
using System.Diagnostics; // Process
using System.Threading; // Thread

namespace MADHouse
{
    class UnityHwndHost : HwndHost
    {
        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] // TODO: 32/64?
        internal static extern IntPtr GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);
        internal const Int32 GWLP_USERDATA = -21;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        internal const UInt32 WM_CLOSE = 0x0010;

        private string fileName;
        private string args;
        private Process process = null;
        private IntPtr unityHWND = IntPtr.Zero;

        public UnityHwndHost(string fileName = "MADHouse3D.exe", string args = "")
        {
            this.fileName = fileName;
            this.args = args;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            Debug.WriteLine("Launching Unity Process: " + this.fileName + " " + this.args);
            process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = args + (args.Length == 0 ? "" : " ") + "-parentHWND " + hwndParent.Handle;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.WaitForInputIdle();

            int repeat = 50;
            while (unityHWND == IntPtr.Zero && repeat-- > 0)
            {
                Thread.Sleep(100);
                EnumChildWindows(hwndParent.Handle, WindowEnum, IntPtr.Zero);
            }
            if (unityHWND == IntPtr.Zero)
                throw new Exception("Unable to find Unity window");
            Debug.WriteLine("Found Unity window: " + unityHWND);

            repeat += 150;
            while ((GetWindowLongPtr(unityHWND, GWLP_USERDATA).ToInt32() & 1) == 0 && --repeat > 0)
            {
                Thread.Sleep(100);
                Debug.WriteLine("Waiting for Unity to initialize... " + repeat);
            }
            if (repeat == 0)
            {
                Debug.WriteLine("Timed out while waiting for Unity to initialize");
            }
            else
            {
                Debug.WriteLine("Unity initialized!");
            }

            return new HandleRef(this, unityHWND);
        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            if (unityHWND != IntPtr.Zero)
                throw new Exception("Found multiple Unity windows");
            unityHWND = hwnd;
            return 0;
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            Debug.WriteLine("Asking Unity to exit...");
            PostMessage(unityHWND, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            int i = 30;
            while (!process.HasExited)
            {
                if (--i < 0)
                {
                    Debug.WriteLine("Process not dead yet, killing...");
                    process.Kill();
                }
                Thread.Sleep(100);
            }
        }
    }
}
