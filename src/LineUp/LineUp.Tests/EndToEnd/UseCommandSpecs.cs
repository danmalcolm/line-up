using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LineUp.Tests.Common;
using NUnit.Framework;

namespace LineUp.Tests.EndToEnd.UseCommandSpecs
{
    public class when_using_framework_version : CommandLineTestingContext
    {
        protected override IEnumerable<string> Commands
        {
            get
            {
                return new[]
                {
                    SetupCommands.ChangeToLineUpTestingDirectory,
                    SetupCommands.SetOriginalPathCommand,
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
            AssertPathContainsExpectedLocations(lineOfOutputContainingPath, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\");
        }
    }

    public class when_requesting_one_version_followed_by_another_version_of_same_component : CommandLineTestingContext
    {
        protected override IEnumerable<string> Commands
        {
            get
            {
                return new[]
                {
                    SetupCommands.ChangeToLineUpTestingDirectory,
                    SetupCommands.SetOriginalPathCommand,
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
            AssertPathContainsExpectedLocations(lineOfOutputContainingPath, "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\");
        }
    }
}