using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;

namespace SplashScreen
{
    class CustomApp
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var splashscren = new SplashScreen(true);

            Thread.Sleep(1000);

            var mainApplication = new System.Windows.Application();
            mainApplication.Startup += (s, e) =>
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                splashscren.Dispose();
            };
            mainApplication.Run();
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(HandleRef hWnd);
    }
}
