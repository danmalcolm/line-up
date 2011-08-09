using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using LineUp.Tests.Common;
using LineUp.Tests.Common.CommandLineTesting;
using LineUp.Utility;
using NUnit.Framework;

namespace LineUp.Tests.EndToEnd
{
    public abstract class CommandLineTestingContext : ContextSpecification
    {
        public class SetupCommands
        {
            public static readonly string SetOriginalPathCommand = string.Format("set path={0}", OriginalPath);
            public static readonly string ChangeToLineUpTestingDirectory = string.Format("cd {0}", PathToLineUp);
        }

        protected const string OriginalPath = "C:\\Program Files\\Common Files\\Microsoft Shared\\Windows Live;C:\\Program Files (x86)\\Common Files\\Microsoft Shared\\Windows Live;C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\";

        protected static readonly string PathToLineUp =
            PathUtility.CombineWithAppDomainPath(ConfigurationManager.AppSettings["PathToLineUp"]);

        protected CommandSessionResult Result;

        protected void AssertPathContainsExpectedLocations(string path, params string[] additionalLocations)
        {
            var actualLocations = new PathEnvironmentVariable(path).Locations;
            var expectedLocations =
                new PathEnvironmentVariable(OriginalPath).AddLocations(additionalLocations).Locations;
            CollectionAssert.AreEquivalent(expectedLocations, actualLocations);
        }

        protected void AssertPathContainsOnlyLocationsInOriginalPath(string path)
        {
            var actualLocations = new PathEnvironmentVariable(path).Locations;
            var expectedLocations = new PathEnvironmentVariable(OriginalPath).Locations;
            CollectionAssert.AreEquivalent(expectedLocations, actualLocations);
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
}