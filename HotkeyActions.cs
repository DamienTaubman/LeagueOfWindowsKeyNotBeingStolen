using LeaugeOfWindowsKeyNotBeingStolen.enums;
using LeaugeOfWindowsKeyNotBeingStolen.hooks;

namespace LeaugeOfWindowsKeyNotBeingStolen
{
    public static class HotkeyActions
    {
        public static readonly Dictionary<int, Action> _hotkeyActions = new Dictionary<int, Action> {
            { (int)VirtualKeys.LWin, ActiveWindowMonitor.BringDesktopToFront },
            { (int)VirtualKeys.RWin, ActiveWindowMonitor.BringDesktopToFront },
        };

        public static Func<int, Action?> getHotkeyAction = (int hotkeyId) => _hotkeyActions.TryGetValue(hotkeyId, out Action? action) ? action : null;
    }
}
