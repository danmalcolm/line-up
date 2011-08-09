using System;

namespace LineUp.Tests.Common
{
    public class PathEnvironmentVariableUtility
    {
        public static string[] GetLocationsInPath(string path)
        {
            return path.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
        }

        
    }
}