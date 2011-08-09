using System;
using System.Text;
using LineUp.Configuration;

namespace LineUp.Commands
{
    public class CommandContext
    {
        private readonly StringBuilder fileContent = new StringBuilder();

        public CommandContext(Config config, string[] args)
        {
            Config = config;
            Args = args;
        }

        public Config Config { get; private set; }

        public string[] Args { get; private set; }


        public void AddLinesToFileExecutedByLauncherBatchFile(string[] lines)
        {
            foreach (var line in lines)
            {
                fileContent.AppendLine(line);
            }
        }

        public string GetContentForFileExecutedByLauncherBatchFile()
        {
            return fileContent.ToString();
        }
    }
}