using System.Diagnostics;
using System.Runtime.InteropServices;
using LeaugeOfWindowsKeyNotBeingStolen.enums;
using LeaugeOfWindowsKeyNotBeingStolen.native;

namespace LeaugeOfWindowsKeyNotBeingStolen.hooks
{
    sealed internal class KeyIntercepter
    {
        private static IntPtr _keyboardHookID = IntPtr.Zero;

        public static void InitializeHooks()
        {
            _keyboardHookID = SetHook(KeyboardEvent.LowLevelKeyboardHook);
        }

        private static IntPtr SetHook(KeyboardEvent hookType)
        {
            Debug.WriteLine("Installing Hook");
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule? curModule = curProcess.MainModule) {
                return Dll.SetWindowsHookEx((int)hookType, KeyboardHookCallback, Dll.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void SetHooksBasedOnAppFocus(bool isTargetAppActive)
        {
            _keyboardHookID = isTargetAppActive && _keyboardHookID == IntPtr.Zero
                ? SetHook(KeyboardEvent.LowLevelKeyboardHook)
                : UninstallHooks();
        }

        public static IntPtr UninstallHooks()
        {
            if (_keyboardHookID == IntPtr.Zero) {
                return _keyboardHookID;
            }

            Debug.WriteLine("Uninstalling Hook");
            return Dll.UnhookWindowsHookEx(_keyboardHookID) ? IntPtr.Zero : _keyboardHookID;
        }

        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && Program.activeWindowTitle == "League of Legends" && wParam == (IntPtr)KeyboardEvent.KeyUp) {
                int vkCode = Marshal.ReadInt32(lParam);
                HotkeyActions.getHotkeyAction(vkCode)?.Invoke();
            }

            return Dll.CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }
    }
}
