using NUnit.Framework;

namespace LineUp.Tests.Common.CommandLineTesting.CommandSessionResultSpecs
{
    public class when_creating_from_standard_output_containing_sequence_of_commands_with_single_line_responses : ContextSpecification
    {
        private CommandSessionResult result;

        protected override void because()
        {
            const string output = @"Microsoft Windows [Version 6.1.7601]
Copyright (c) 2009 Microsoft Corporation.  All rights reserved.

C:\Users\Dan\Dev\Mine>echo command 1
command 1

C:\Users\Dan\Dev\Mine>echo command 2
command 2

C:\Users\Dan\Dev\Mine>echo command 3
command 3

C:\Users\Dan\Dev\Mine>echo command 4
command 4

C:\Users\Dan\Dev\Mine>";
            result = new CommandSessionResult(output, "");
        }

        [Test]
        public void should_contain_response_for_first_command()
        {
            result.GetResponseForCommand(0).ShouldEqual("command 1");
        }

        [Test]
        public void should_contain_response_for_middle_commands()
        {
            result.GetResponseForCommand(1).ShouldEqual("command 2");
            result.GetResponseForCommand(2).ShouldEqual("command 3");
        }

        [Test]
        public void should_contain_response_for_last_command()
        {
            result.GetResponseForCommand(3).ShouldEqual("command 4");
        }
    }
    public class when_creating_from_standard_output_containing_sequence_of_commands_with_multiple_line_responses : ContextSpecification
    {
        private CommandSessionResult result;

        protected override void because()
        {
            const string output = @"Microsoft Windows [Version 6.1.7601]
Copyright (c) 2009 Microsoft Corporation.  All rights reserved.

C:\Users\Dan\Dev\Mine>dir
 Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

03/08/2011  19:18    <DIR>          .
03/08/2011  19:18    <DIR>          ..
23/06/2011  03:22    <DIR>          AutoDeploymentSample
22/07/2011  20:57    <DIR>          Dev-Tools
30/06/2011  21:05    <DIR>          FluentNHibernateDemo
24/07/2011  21:21    <DIR>          friends-of-castle-park
04/07/2011  19:12    <DIR>          GraphsInNHibernate
03/08/2011  19:18    <DIR>          line-up
27/06/2011  23:57    <DIR>          Phoebus
20/07/2011  23:36    <DIR>          Rails301
               0 File(s)              0 bytes
              10 Dir(s)  400,015,613,952 bytes free

C:\Users\Dan\Dev\Mine>dir /W
 Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

[.]                      [..]                     [AutoDeploymentSample]
[Dev-Tools]              [FluentNHibernateDemo]   [friends-of-castle-park]
[GraphsInNHibernate]     [line-up]                [Phoebus]
[Rails301]
               0 File(s)              0 bytes
              10 Dir(s)  400,015,609,856 bytes free

C:\Users\Dan\Dev\Mine>dir /X
 Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

03/08/2011  19:18    <DIR>                       .
03/08/2011  19:18    <DIR>                       ..
23/06/2011  03:22    <DIR>          AUTODE~1     AutoDeploymentSample
22/07/2011  20:57    <DIR>          DEV-TO~1     Dev-Tools
30/06/2011  21:05    <DIR>          FLUENT~1     FluentNHibernateDemo
24/07/2011  21:21    <DIR>          FRIEND~1     friends-of-castle-park
04/07/2011  19:12    <DIR>          GRAPHS~1     GraphsInNHibernate
03/08/2011  19:18    <DIR>                       line-up
27/06/2011  23:57    <DIR>                       Phoebus
20/07/2011  23:36    <DIR>                       Rails301
               0 File(s)              0 bytes
              10 Dir(s)  400,015,650,816 bytes free

C:\Users\Dan\Dev\Mine>";
            result = new CommandSessionResult(output, "");
        }

        [Test]
        public void should_contain_response_for_first_command()
        {
            result.GetResponseForCommand(0).ShouldEqual(@" Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

03/08/2011  19:18    <DIR>          .
03/08/2011  19:18    <DIR>          ..
23/06/2011  03:22    <DIR>          AutoDeploymentSample
22/07/2011  20:57    <DIR>          Dev-Tools
30/06/2011  21:05    <DIR>          FluentNHibernateDemo
24/07/2011  21:21    <DIR>          friends-of-castle-park
04/07/2011  19:12    <DIR>          GraphsInNHibernate
03/08/2011  19:18    <DIR>          line-up
27/06/2011  23:57    <DIR>          Phoebus
20/07/2011  23:36    <DIR>          Rails301
               0 File(s)              0 bytes
              10 Dir(s)  400,015,613,952 bytes free");
        }

        [Test]
        public void should_contain_response_for_middle_command()
        {
            result.GetResponseForCommand(1).ShouldEqual(@" Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

[.]                      [..]                     [AutoDeploymentSample]
[Dev-Tools]              [FluentNHibernateDemo]   [friends-of-castle-park]
[GraphsInNHibernate]     [line-up]                [Phoebus]
[Rails301]
               0 File(s)              0 bytes
              10 Dir(s)  400,015,609,856 bytes free");
        }

        [Test]
        public void should_contain_response_for_last_command()
        {
            result.GetResponseForCommand(2).ShouldEqual(@" Volume in drive C is OS
 Volume Serial Number is FC40-91E3

 Directory of C:\Users\Dan\Dev\Mine

03/08/2011  19:18    <DIR>                       .
03/08/2011  19:18    <DIR>                       ..
23/06/2011  03:22    <DIR>          AUTODE~1     AutoDeploymentSample
22/07/2011  20:57    <DIR>          DEV-TO~1     Dev-Tools
30/06/2011  21:05    <DIR>          FLUENT~1     FluentNHibernateDemo
24/07/2011  21:21    <DIR>          FRIEND~1     friends-of-castle-park
04/07/2011  19:12    <DIR>          GRAPHS~1     GraphsInNHibernate
03/08/2011  19:18    <DIR>                       line-up
27/06/2011  23:57    <DIR>                       Phoebus
20/07/2011  23:36    <DIR>                       Rails301
               0 File(s)              0 bytes
              10 Dir(s)  400,015,650,816 bytes free");
        }
    }
}