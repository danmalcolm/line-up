using System.Collections.Generic;
using System.Linq;

namespace LineUp.Configuration
{
    public class Config
    {
        public Config(IEnumerable<Component> components)
        {
           Components = components.ToList().AsReadOnly();
        }

        public IEnumerable<Component> Components { get; private set; }
    }

    public class Component
    {
        public Component(string name, IEnumerable<ComponentVersion> versions)
        {
            Name = name;
            Versions = versions;
        }

        public string Name { get; private set; }

        public IEnumerable<ComponentVersion> Versions { get; private set; }
    }

    public class ComponentVersion
    {
        public ComponentVersion(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
    }
}