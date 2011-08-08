using System;
using System.IO;

namespace LineUp.Tests.Common
{
    public class PathUtility
    {
        public static string CombineWithAppDomainPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            string appDomainPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            return Path.GetFullPath(appDomainPath);
        }
    }
}