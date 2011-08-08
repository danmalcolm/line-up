using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LineUp.Tests.Common.CommandLineTesting
{
    /// <summary>
    /// Encapsulates interaction with Windows command console cmd.exe
    /// </summary>
    public class CommandSession
    {
        public static CommandSessionResult Execute(IEnumerable<string> commands)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            using (var process = Process.Start(processStartInfo))
            {
                string input = string.Join(Environment.NewLine, commands) + Environment.NewLine;
                process.StandardInput.Write(input);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                return new CommandSessionResult(process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd());
            }
        }
    }
}