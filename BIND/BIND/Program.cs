﻿using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace BIND
{
    class Macro
    {
        public static List<Macro> ReadFile()
        {
            List<Macro> output = new List<Macro>();
            string macrolist = File.ReadAllText(@"C:\Macros.txt");
            string[] macrolines = macrolist.Split('\n');
            Directory.SetCurrentDirectory(@"C:\BINDshortcuts");
            foreach (string x in macrolines)
            {
                string[] parts = x.Split('|');
                parts[1] = Directory.GetCurrentDirectory() + @"\" + parts[1];
                output.Add(new Macro(parts[0], parts[1], parts[2], (parts[3] == "true"), (parts[4] == "true"), (parts[5] == "true")));
            }
            return output;
        }

        public Macro(string keyname, string appname, string runarg, bool alt, bool ctrl, bool shift)
        {
            _keyname = keyname;
            _a = appname;
            _r = runarg;
            _alt = alt;
            _ctrl = ctrl;
            _shift = shift;
            _action = (a, r, alt_, ctrl_, shift_, currentalt, currentctrl, currentshift) =>
            {
                if (alt_ == currentalt && ctrl_ == currentctrl && shift_ == currentshift)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = a;
                    process.StartInfo.Arguments = r;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                }
            };
        }

        public void Trigger(bool currentalt, bool currentctrl, bool currentshift) { _action.Invoke(_a, _r, _alt, _ctrl, _shift, currentalt, currentctrl, currentshift); }
        public string GetKeyname() { return _keyname; }
        private readonly string _keyname;
        private readonly string _a;
        private readonly string _r;
        private readonly bool _alt, _ctrl, _shift;
        private readonly Action<string, string, bool, bool, bool, bool, bool, bool> _action;
    }


    class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WM_MBUTTONDOWN = 0x0207;
        public static bool alt;
        public static bool ctrl;
        public static bool shift;
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
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN || wParam == (IntPtr)WM_MBUTTONDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == 164) alt = true;
                else if (vkCode == 160) shift = true;
                else if (vkCode == 162) ctrl = true;
                else
                {
                    Console.WriteLine((Keys)vkCode);
                    foreach (Macro x in macros) { if (((Keys)vkCode).ToString() == x.GetKeyname()) x.Trigger(alt, ctrl, shift); }
                }
            }
            else if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode == 164) alt = false;
                else if (vkCode == 160) shift = false;
                else if (vkCode == 162) ctrl = false;
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
