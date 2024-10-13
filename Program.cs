using LeaugeOfWindowsKeyNotBeingStolen.hooks;

namespace LeaugeOfWindowsKeyNotBeingStolen
{
    internal class Program
    {
        public static string activeWindowTitle = "unset";

        [STAThread]
        static void Main(string[] args)
        {
            ActiveWindowMonitor activeWindowMonitor = new ActiveWindowMonitor((bool isTargetAppActive) => {
                KeyIntercepter.SetHooksBasedOnAppFocus(isTargetAppActive);
            });
            activeWindowMonitor.StartListeningForWindowChanges();

            Application.Run();
            activeWindowMonitor.StopListeningForWindowChanges();
        }
    }
}