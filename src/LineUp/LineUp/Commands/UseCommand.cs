﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using LineUp.Configuration;

namespace LineUp.Commands
{
    public class UseCommand
    {
        private static readonly Config Config = DemoConfigBuilder.Build();

        public void Execute(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Expected 2 arguments, e.g. lu use .net 3.5");
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

            WriteNewPathToFileUsedByLauncherBatchFile(newPath);

            Console.WriteLine(@"OK: Using path ""{0}"" for component ""{1}"", version ""{2}""", version.Path, component.Name, version.Name);
        }

        private static void WriteNewPathToFileUsedByLauncherBatchFile(string newPath)
        {
            string filePath = Environment.ExpandEnvironmentVariables("%TEMP%\\LineUp\\modify_path.bat");
            if(!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var content = new StringBuilder();
            content.AppendLine(
                "REM this script is generated by LineUp so that it can be executed by the launcher batch file, allowing changes to be made to the path within the current command window session");
            content.AppendFormat("set path={0}", newPath);
            content.AppendLine();
            File.WriteAllText(filePath, content.ToString(), Encoding.ASCII);
        }
    }
}