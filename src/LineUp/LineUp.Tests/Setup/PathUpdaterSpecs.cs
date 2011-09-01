using System;
using LineUp.Setup.CustomActions;
using NUnit.Framework;

namespace LineUp.Tests.Setup
{
    [TestFixture]
    public class PathUpdaterSpecs
    {
        [Test]
         public void can_read_machine_path()
        {
            Console.WriteLine(PathUpdater.GetMachinePath());
        }
    }
}