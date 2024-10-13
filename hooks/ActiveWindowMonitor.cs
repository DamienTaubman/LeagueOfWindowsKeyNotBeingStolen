using System.Diagnostics;
using LeaugeOfWindowsKeyNotBeingStolen.native;

namespace LeaugeOfWindowsKeyNotBeingStolen.hooks
{
    sealed internal class ActiveWindowMonitor
    {
        private static IntPtr winHook;
        private WinEventProc? listener;

        private readonly Action<bool> applicationStatusChanged;

        public ActiveWindowMonitor(Action<bool> onApplicationStatusChanged)
        {
            applicationStatusChanged = onApplicationStatusChanged;
        }

        public void StartListeningForWindowChanges()
        {
            Console.WriteLine("Listening for window changes...");
            listener = new WinEventProc(EventCallback);

            winHook = Dll.SetWinEventHook(3, 3, IntPtr.Zero, listener, 0, 0, 0);
        }

        public Action StopListeningForWindowChanges = () => Dll.UnhookWinEvent(winHook);

        private void EventCallback(IntPtr hWinEventHook, uint iEvent, IntPtr hWnd, int idObject, int idChild, int dwEventThread, int dwmsEventTime)
        {
            uint processId;
            Dll.GetWindowThreadProcessId(hWnd, out processId);
            Process process = Process.GetProcessById((int)processId);
            Console.WriteLine($"Active Application is now: {process.ProcessName}");
            Program.activeWindowTitle = process.ProcessName;
            bool isTargetAppActive = process.ProcessName.Equals("League of Legends", StringComparison.OrdinalIgnoreCase);
            applicationStatusChanged?.Invoke(isTargetAppActive);
        }

        public static void BringDesktopToFront()
        {
            IntPtr desktopHandle = Dll.GetShellWindow();
            if (desktopHandle != IntPtr.Zero) {
                Debug.WriteLine("Bringing desktop to the front");
                Dll.SetForegroundWindow(desktopHandle);
            }
        }
    }
}
