using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;

namespace SplashScreen
{
    public class SplashScreen : IDisposable
    {
        private Dispatcher _dispatcher;
        private readonly Thread _thread;
        private SplashWindow _instance;
        private readonly bool _async = true;

        public SplashScreen(bool async)
        {
            _async = async;

            if (_async)
            {
                _thread = new Thread(() => Run())
                {
                    Name = "SplashScreen",
                    IsBackground = true
                };

                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Start();
            }
            else
            {
                Run();
            }
        }

        public void Run()
        {
            _instance = new SplashWindow();
            _instance.Show();

            if (_async)
            {
                _dispatcher = Dispatcher.CurrentDispatcher;
                Dispatcher.Run();
            }
        }

        public void Dispose()
        {
            if (_async)
            {
                _thread.Abort();
                _dispatcher?.BeginInvokeShutdown( DispatcherPriority.Background);
                _thread.Join();
            }
            else
            {
                _instance.Close();
            }
        }
    }
}
