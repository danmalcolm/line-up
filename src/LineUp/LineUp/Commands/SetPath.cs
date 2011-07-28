using System;
using System.IO;
using System.Linq;
using LineUp.Configuration;

namespace LineUp.Commands
{
    public class SetPath
    {
        private static readonly Config Config = DemoConfigBuilder.Build();

        public void Execute(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Expected 2 arguments, e.g. lu set .net 3.5, or lu .net 35");
                return;
            }
            string componentName = args[0];
            string versionName = args[1];
            var component = Config.Components.SingleOrDefault(x => x.Name == componentName);
            if (component == null)
            {
                Console.WriteLine(@"WARNING: No component could be found with the name ""{0}""", componentName);
                return;
            }
            var version = component.Versions.SingleOrDefault(x => x.Name == versionName);
            if (version == null)
            {
                Console.WriteLine(@"WARNING: No version called ""{0}"" exists in component ""{1}""", versionName, componentName);
                return;
            }
            string newPath = PathMonkeyer.SetComponentVersion(component, version);

            WriteNewPathToFileUsedByBatchFile(newPath);

            Console.WriteLine(@"OK: Set path ""{0}"" for component ""{1}"", version ""{2}""", version.Path, component.Name, version.Name);
        }

        private static void WriteNewPathToFileUsedByBatchFile(string newPath)
        {
            string profilePath = Environment.GetEnvironmentVariable("USERPROFILE", EnvironmentVariableTarget.Process);
            if(profilePath == null)
            {
                Console.WriteLine("WARNING: Unable to get profile path from USERPROFILE environment variable");
            }
            else
            {
                string path = Path.Combine(profilePath, "LineUp.NewPath.temp");
                File.WriteAllText(path, newPath);
            }
        }
    }
}