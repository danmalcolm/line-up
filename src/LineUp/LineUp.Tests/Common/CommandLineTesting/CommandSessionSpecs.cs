using System;
using NUnit.Framework;

namespace LineUp.Tests.Common.CommandLineTesting.CommandSessionSpecs
{
    public class when_executing_sequence_of_valid_commands : ContextSpecification
    {
        private CommandSessionResult result;

        protected override void because()
        {
            var commands = new[]  {"echo result of command 1", "echo result of command 2", "echo result of command 3" };
            result = CommandSession.Execute(commands);
            Console.Write(result.EntireStandardOutput);
        }

        [Test]
        public void result_should_contain_standard_output_from_console()
        {
            result.EntireStandardOutput.ShouldNotBeEmpty();
        }

        [Test]
        public void result_should_contain_empty_standard_error()
        {
            result.EntireStandardError.ShouldBeEmpty();
        }

        [Test]
        public void result_should_contain_response_to_first_command_from_console_output()
        {
            result.GetResponseForCommand(0).ShouldEqual("result of command 1");
        }

        [Test]
        public void result_should_contain_response_to_middle_command_from_console_output()
        {
            result.GetResponseForCommand(1).ShouldEqual("result of command 2");
        }

        [Test]
        public void result_should_contain_response_to_last_command_from_console_output()
        {
            result.GetResponseForCommand(2).ShouldEqual("result of command 3");
        }


    }


    public class when_executing_sequence_containing_invalid_command : ContextSpecification
    {
        private CommandSessionResult result;

        protected override void because()
        {
            var commands = new[] { "echo result of command 1", "wtf", "echo result of command 3", "echo result of command 4" };
            result = CommandSession.Execute(commands);
            Console.Write(result.EntireStandardOutput);
            Console.Write(result.EntireStandardError);
        }

        [Test]
        public void result_should_contain_standard_output_from_console()
        {
            result.EntireStandardOutput.ShouldNotBeEmpty();
        }

        [Test]
        public void result_should_contain_standard_error_from_console()
        {
            result.EntireStandardError.ShouldNotBeEmpty();
        }

        [Test]
        public void result_should_indicate_that_error_occured()
        {
            result.HasErrors.ShouldBeTrue();
        }

        [Test]
        public void result_should_contain_response_to_first_valid_command_from_console_output()
        {
            result.GetResponseForCommand(0).ShouldEqual("result of command 1");
        }

        [Test]
        public void result_should_contain_response_to_middle_valid_command_from_console_output()
        {
            result.GetResponseForCommand(2).ShouldEqual("result of command 3");
        }

        [Test]
        public void response_to_invalid_command_should_be_empty()
        {
            result.GetResponseForCommand(1).ShouldBeEmpty();
        }

        [Test]
        public void result_should_contain_response_to_last_valid_command_from_console_output()
        {
            result.GetResponseForCommand(3).ShouldEqual("result of command 4");
        }


    }
}