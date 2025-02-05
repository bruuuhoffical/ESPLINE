using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AotForms
{
    internal static class Program
    {
        [UnmanagedCallersOnly(EntryPoint = "Load")]
        public static void Load(nint pVM)
        {
            InternalMemory.Initialize(pVM);
            
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();

            var process = Process.GetCurrentProcess();

            Application.Run(new Form2(process.MainWindowHandle));

            Application.Run(new Form1(IntPtr.Zero));
        }
    }
}
