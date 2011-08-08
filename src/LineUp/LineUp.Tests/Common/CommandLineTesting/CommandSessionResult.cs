using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LineUp.Tests.Common.CommandLineTesting
{
    public class CommandSessionResult
    {
        private readonly List<string> responses;

        public CommandSessionResult(string standardOutput, string standardError)
        {
            EntireStandardOutput = standardOutput;
            EntireStandardError = standardError;
            responses = GetResponsesFromOutput(standardOutput).ToList();
        }

        public string EntireStandardOutput { get; private set; }

        public string EntireStandardError { get; private set; }

        public bool HasErrors
        {
            get { return !string.IsNullOrWhiteSpace(EntireStandardError); }
        }

        private IEnumerable<string> GetResponsesFromOutput(string standardOutput)
        {
            var outputLines = standardOutput.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            var linesWithIndexes = outputLines
                .Select((line, index) => new {Index = index, Line = line});
            
            return from lineWithIndex in linesWithIndexes
                   where IsInputLine(lineWithIndex.Line)
                   let responseLines = outputLines
                       .Skip(lineWithIndex.Index + 1)
                       .TakeWhile(line => !IsInputLine(line))
                   let response = CombineResponseLines(responseLines)
                   select response;
        }

        private string CombineResponseLines(IEnumerable<string> lines)
        {
            // trim blank lines from end
            IEnumerable<string> trimmed = lines.Reverse().SkipWhile(line => string.IsNullOrWhiteSpace(line)).Reverse();
            return string.Join(Environment.NewLine, trimmed);
        }

        /// <summary>
        /// Indicates whether line of output represents user input, e.g. 
        /// C:\>dosomething.bat
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static bool IsInputLine(string line)
        {
            var regex = new Regex(@"\b[a-z]:\\
(?:[^\\/:*?""<>|\r\n]+\\)* # Folders
(?:[^\\/:*?""<>|\r\n]*)> # Last folder", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            return regex.IsMatch(line);
        }

        public string GetResponseForCommand(int commandIndex)
        {
//            if (HasErrors)
//            {
//                throw new InvalidOperationException("Cannot parse responses when error has occurred");
//            }
            return responses[commandIndex];
        }
    }
}