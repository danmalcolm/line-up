using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using LineUp.Tests.Common;
using LineUp.Tests.Common.CommandLineTesting;
using NUnit.Framework;
using System.Linq;

namespace LineUp.Tests.EndToEnd.UseCommandSpecs
{
    public abstract class context_for_testing_using_command_line : ContextSpecification
    {
        protected const string InitialPath = "C:\\Program Files\\Common Files\\Microsoft Shared\\Windows Live;C:\\Program Files (x86)\\Common Files\\Microsoft Shared\\Windows Live;C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\";

        protected static readonly string PathToLineUp = PathUtility.CombineWithAppDomainPath(ConfigurationManager.AppSettings["PathToLineUp"]);

        protected void AssertPathContainsExpectedItems(string path, params string[] additionalPaths)
        {
            var actualItems = EnvironmentUtility.GetItemsInPath(path);
            var expectedItems = EnvironmentUtility.GetItemsInPath(InitialPath)
                .Concat(additionalPaths).ToList();
            CollectionAssert.AreEquivalent(expectedItems, actualItems);
        }
    }

    public class when_using_framework_version : context_for_testing_using_command_line
    {
        private CommandSessionResult result;

        protected override void because()
        {
            var commands = new List<string>
            {
                string.Format("set path={0}", InitialPath),
                string.Format("cd {0}", PathToLineUp),
                "lu use .net 3.5",
                "echo %path%",
                "lu use .net 4.0",
                "echo %path%"
            };
            result = CommandSession.Execute(commands);
            Console.WriteLine(result.EntireStandardOutput);
        }

        [Test]
        public void error_should_not_occur()
        {
            result.HasErrors.ShouldBeFalse();
        }

        [Test]
        public void path_environment_variable_should_combine_path_for_framework_with_original_path()
        {
            var outputContainingPath = result.GetResponseForCommand(5);
            AssertPathContainsExpectedItems(outputContainingPath, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\");
        }
    }
}