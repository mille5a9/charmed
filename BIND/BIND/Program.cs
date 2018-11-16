using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BIND
{
    class Macro
    {
        public static List<Macro> ReadFile()
        {
            List<Macro> output = new List<Macro>();
            string macrolist = File.ReadAllText(@"C:\Macros.txt");
            string[] macrolines = macrolist.Split('\n');
            foreach (string x in macrolines)
            {
                string[] parts = x.Split('|');
                output.Add(new Macro(parts[0], parts[1], parts[2]));
            }
            return output;
        }

        public Macro(string keyname, string appname, string runarg)
        {
            _keyname = keyname;
            _a = appname;
            _r = runarg;
            _action = (a, r) =>
            {
                Process.Start(a, r);
            };
        }
        public void Trigger() { _action.Invoke(_a, _r); }
        public string GetKeyname() { return _keyname; }
        private readonly string _keyname;
        private readonly string _a;
        private readonly string _r;
        private readonly Action<string, string> _action;
    }


    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static List<Macro> macros;

        public static void Main()
        {
            macros = Macro.ReadFile();

            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Console.WriteLine((Keys)vkCode);
                foreach (Macro x in macros) { if (((Keys)vkCode).ToString() == x.GetKeyname()) x.Trigger(); }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

}
