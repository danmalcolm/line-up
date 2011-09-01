using Microsoft.Win32;

namespace LineUp.Setup.CustomActions
{
    public class PathUpdater
    {
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public static string GetMachinePath()
        {
            using(var key = GetSystemPathRegKey(true))
            {
                return ReadPathFromKey(key);
            }
        }
        private static RegistryKey GetSystemPathRegKey(bool writable)
        {
            // for the system-wide path...
            return Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", writable);
        }

        private static string ReadPathFromKey(RegistryKey key)
        {
            return (string)key.GetValue("Path", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
        }

        public static void SetMachinePath(string path)
        {
            using(var key = GetSystemPathRegKey(true))
            {
                key.SetValue("Path", path, RegistryValueKind.ExpandString);
            }
        }
    }
}