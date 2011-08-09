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
        public class SetupCommands
        {
            public static readonly string SetInitialPathCommand = string.Format("set path={0}", InitialPath);
            public static readonly string ChangeToLineUpTestingDirectory = string.Format("cd {0}", PathToLineUp);
        }

        protected const string InitialPath = "C:\\Program Files\\Common Files\\Microsoft Shared\\Windows Live;C:\\Program Files (x86)\\Common Files\\Microsoft Shared\\Windows Live;C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\";

        protected static readonly string PathToLineUp =
            PathUtility.CombineWithAppDomainPath(ConfigurationManager.AppSettings["PathToLineUp"]);

        protected CommandSessionResult Result;

        protected void AssertPathContainsExpectedItems(string path, params string[] additionalPaths)
        {
            var actualItems = EnvironmentUtility.GetItemsInPath(path);
            var expectedItems = EnvironmentUtility.GetItemsInPath(InitialPath)
                .Concat(additionalPaths).ToList();
            CollectionAssert.AreEquivalent(expectedItems, actualItems);
        }

               
        protected override void because()
        {
            Result = CommandSession.Execute(Commands);
            Console.WriteLine("Standard output:");
            Console.WriteLine(Result.EntireStandardOutput);
            Console.WriteLine("----------------------------");
            Console.WriteLine("Standard error:");
            Console.WriteLine(Result.EntireStandardError);
            Console.WriteLine("----------------------------");
        }

        protected abstract IEnumerable<string> Commands { get; }
    }

    public class when_using_framework_version : context_for_testing_using_command_line
    {
        protected override IEnumerable<string> Commands
        {
            get
            {
                return new[]
                {
                    SetupCommands.ChangeToLineUpTestingDirectory,
                    SetupCommands.SetInitialPathCommand,
                    "lu use .net 4.0",
                    "echo %path%"
                };
            }
        }

        [Test]
        public void error_should_not_occur()
        {
            Result.HasErrors.ShouldBeFalse();
        }

        [Test]
        public void environment_variable_should_combine_path_for_requested_version_with_original_paths()
        {
            var lineOfOutputContainingPath = Result.GetResponseForCommand(3);
            AssertPathContainsExpectedItems(lineOfOutputContainingPath, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\");
        }
    }

    public class when_requesting_one_version_followed_by_another_version_of_same_component : context_for_testing_using_command_line
    {
        protected override IEnumerable<string> Commands
        {
            get
            {
                return new[]
                {
                    SetupCommands.ChangeToLineUpTestingDirectory,
                    SetupCommands.SetInitialPathCommand,
                    "lu use .net 3.5",
                    "lu use .net 4.0",
                    "echo %path%"
                };
            }
        }

        [Test]
        public void error_should_not_occur()
        {
            Result.HasErrors.ShouldBeFalse();
        }

        [Test]
        public void path_for_last_requested_version_should_be_combined_with_paths_in_original_environment_variable()
        {
            var lineOfOutputContainingPath = Result.GetResponseForCommand(4);
            AssertPathContainsExpectedItems(lineOfOutputContainingPath, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\");
        }
    }
}