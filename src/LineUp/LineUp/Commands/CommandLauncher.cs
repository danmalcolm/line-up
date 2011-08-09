using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LineUp.Configuration;

namespace LineUp.Commands
{
    public class CommandLauncher
    {
        private readonly Config config;
        private readonly string[] args;

        public CommandLauncher(Config config, string[] args)
        {
            this.config = config;
            this.args = args;
        }

        public void Execute()
        {
            ShowIntro();

            if (!args.Any())
            {
                ShowUsage();
                return;
            }

            string commandName = args.First().ToLowerInvariant();
            switch (commandName)
            {
                case "use":
                    ExecuteCommand(new UseCommand());
                    break;
                case "revert":
                    ExecuteCommand(new RevertCommand());
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        private void ExecuteCommand(ICommand command)
        {
            var commandArgs = args.Skip(1).ToArray();
            var context = new CommandContext(config, commandArgs);
            command.Execute(context);

            WriteContentToFileExecutedByLauncherBatchFile(context.GetContentForFileExecutedByLauncherBatchFile());
        }

        private static void WriteContentToFileExecutedByLauncherBatchFile(string content)
        {
            string filePath = Environment.ExpandEnvironmentVariables("%TEMP%\\LineUp\\modify_path.bat");
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var fileContent = new StringBuilder();
            fileContent.AppendLine(
                "REM this script is generated by LineUp so that it can be executed by the launcher batch file, allowing changes to be made to environment variables within the current command window session");
            fileContent.Append(content);
            File.WriteAllText(filePath, fileContent.ToString(), Encoding.ASCII);
        }

        private static void ShowIntro()
        {
            Console.WriteLine(@"Running LineUp");
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Unable to work out what you're trying to do.");
            Console.WriteLine(@"Example usage: lu use <component> <version>");
        }
    }
}