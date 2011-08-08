using System;

namespace LineUp.Tests.Common
{
    public class EnvironmentUtility
    {
        public static string[] GetItemsInPath(string path)
        {
            return path.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}