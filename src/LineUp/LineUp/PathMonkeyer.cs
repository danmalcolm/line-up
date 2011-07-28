using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using LineUp.Configuration;
using Component = LineUp.Configuration.Component;

namespace LineUp
{
    public class PathMonkeyer
    {
         public static string SetComponentVersion(Component component, ComponentVersion version)
         {
             var path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process) ?? "";
             var paths = path.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).Except(component.Versions.Select(x => x.Path));
             var newPath = string.Join(";", paths.Concat(new[] {version.Path}).ToArray());
             return newPath;
         }
    }
}