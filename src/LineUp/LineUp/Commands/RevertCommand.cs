using System;
using LineUp.Utility;

namespace LineUp.Commands
{
    public class RevertCommand : ICommand
    {
        #region ICommand Members

        public void Execute(CommandContext context)
        {
            string currentPathValue = Environment.GetEnvironmentVariable(EnvironmentVariables.Path,
                                                                         EnvironmentVariableTarget.Process);
            string originalPathValue = Environment.GetEnvironmentVariable(EnvironmentVariables.OriginalPath,
                                                                          EnvironmentVariableTarget.Process);

            bool revertRequired = !string.IsNullOrWhiteSpace(originalPathValue)
                && !new PathEnvironmentVariable(originalPathValue).IsEquivalentTo(currentPathValue);

            if (revertRequired)
            {
                RevertToOriginalPathInCommandLineSession(context, originalPathValue);
                Console.WriteLine(Resources.RevertCommand_RevertingToOriginalPath);
            }
            else
            {
                Console.WriteLine(Resources.RevertCommand_RevertNotRequired);
            }

        }

        #endregion

        private static void RevertToOriginalPathInCommandLineSession(CommandContext context, string originalPath)
        {
            string restorePath = string.Format("set {0}={1}", EnvironmentVariables.Path, originalPath);
            string removeOriginal = string.Format("set {0}=", EnvironmentVariables.OriginalPath);
            context.AddLinesToFileExecutedByLauncherBatchFile(new[] {restorePath, removeOriginal});
        }
    }
}