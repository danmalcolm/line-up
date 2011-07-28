using System.Collections.Generic;

namespace LineUp.Configuration
{
    public class DemoConfigBuilder
    {
        public static Config Build()
        {
            var components = BuildComponents();
            return new Config(components);
        }

        private static IEnumerable<Component> BuildComponents()
        {
            var versions = new List<ComponentVersion>
            {
                new ComponentVersion("2.0", "C:\\Windows\\Microsoft.NET\\Framework\\v2.0.50727\\"),
                new ComponentVersion("3.5", "C:\\Windows\\Microsoft.NET\\Framework\\v3.5\\"),
                new ComponentVersion("4.0", "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\")
            };
            yield return new Component(".net", versions);
        }
    }
}