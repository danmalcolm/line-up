using System.Collections.Generic;
using System.Linq;

namespace LineUp.Tests.Configuration.TestConfiguration
{
    public class DemoConfigBuilder
    {
        public static Config Build()
        {
            var components = BuildComponents();
            return new Config { Components = components.ToList() };
        }

        private static IEnumerable<Component> BuildComponents()
        {
            var versions = new List<ComponentVersion>
            {
                new ComponentVersion { Name = "2.0", Path = "C:\\Windows\\Microsoft.NET\\Framework\\v2.0.50727\\" },
                new ComponentVersion { Name = "3.5", Path = "C:\\Windows\\Microsoft.NET\\Framework\\v3.5\\" } ,
                new ComponentVersion { Name = "4.0", Path = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\" }
            };
            yield return new Component { Name = ".net", Versions = versions };
        }
    }
}