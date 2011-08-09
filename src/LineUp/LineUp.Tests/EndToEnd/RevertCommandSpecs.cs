using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LineUp.Tests.Common;
using NUnit.Framework;

namespace LineUp.Tests.EndToEnd.RevertCommandSpecs
{
    public class when_reverting_after_modifying_path_by_using_framework_version : CommandLineTestingContext
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
                    "echo %path%",
                    "lu revert",
                    "echo %path%",
                };
            }
        }

        [Test]
        public void error_should_not_occur()
        {
            Result.HasErrors.ShouldBeFalse();
        }

        [Test]
        public void environment_variable_should_contain_original_path()
        {
            var lineOfOutputContainingPath = Result.GetResponseForCommand(5);
            AssertPathContainsOnlyLocationsInOriginalPath(lineOfOutputContainingPath);
        }

        [Test]
        public void should_indicate_that_reverted()
        {
            var outputFromRevertCommand = Result.GetResponseForCommand(4);
            outputFromRevertCommand.ShouldContain(Resources.RevertCommand_RevertingToOriginalPath);
        }
    }

    public class when_reverting_before_any_changes_made_to_path : CommandLineTestingContext
    {
        protected override IEnumerable<string> Commands
        {
            get
            {
                return new[]
                {
                    SetupCommands.ChangeToLineUpTestingDirectory,
                    SetupCommands.SetOriginalPathCommand,
                    "lu revert",
                    "echo %path%",
                };
            }
        }

        [Test]
        public void error_should_not_occur()
        {
            Result.HasErrors.ShouldBeFalse();
        }

        [Test]
        public void environment_variable_should_contain_original_path()
        {
            var lineOfOutputContainingPath = Result.GetResponseForCommand(3);
            AssertPathContainsOnlyLocationsInOriginalPath(lineOfOutputContainingPath);
        }

        [Test]
        public void should_indicate_that_revert_not_required()
        {
            var outputFromRevertCommand = Result.GetResponseForCommand(2);
            outputFromRevertCommand.ShouldContain(Resources.RevertCommand_RevertNotRequired);
        }
    }

   
}