using System;
using System.IO;
using System.Linq;
using System.Text;
using LineUp.Utility;

namespace LineUp.Commands
{
    public class UseCommand : ICommand
    {
        public void Execute(CommandContext context)
        {
            var args = context.Args;
            var config = context.Config;
            if(args.Length < 2)
            {
                Console.WriteLine("Expected 2 arguments, e.g. lu use .net 3.5");
                return;
            }
            string componentName = args[0];
            string versionName = args[1];
            var component = config.Components.SingleOrDefault(x => x.Name == componentName);
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

            SaveOriginalPathInCommandLineSession(context);
            string newPath = PathMonkeyer.SetComponentVersion(component, version);

            SetNewPathInCommandLineSession(context, newPath);

            Console.WriteLine(@"INFO: Using path ""{0}"" for component ""{1}"", version ""{2}""", version.Path, component.Name, version.Name);
        }

        /// <summary>
        /// Sets an environment variable storing path before any changes have been made by LineUp
        /// </summary>
        /// <param name="context"></param>
        private static void SaveOriginalPathInCommandLineSession(CommandContext context)
        {
            bool originalPathAlreadySaved = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(EnvironmentVariables.OriginalPath));
            if (!originalPathAlreadySaved)
            {
                var originalPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process) ?? "";
                string command = string.Format("set {0}={1}", EnvironmentVariables.OriginalPath, originalPath);
                context.AddLinesToFileExecutedByLauncherBatchFile(new[] { command });      
            }
        }

        private static void SetNewPathInCommandLineSession(CommandContext context, string newPath)
        {
            string command = string.Format("set path={0}", newPath);
            context.AddLinesToFileExecutedByLauncherBatchFile(new[] { command });
        }
    }
}