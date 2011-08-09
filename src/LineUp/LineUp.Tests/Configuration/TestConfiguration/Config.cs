using System.Collections.Generic;
using System.Xml.Serialization;

namespace LineUp.Tests.Configuration.TestConfiguration
{
    public class Config
    {
        public Config()
        {
            Components = new List<Component>();
        }

        public List<Component> Components { get; set; }
    }

    public class Component
    {
        public Component()
        {
            Versions = new List<ComponentVersion>();
        }

        [XmlAttribute]
        public string Name { get; set; }

        public List<ComponentVersion> Versions { get; set; }
    }

    public class ComponentVersion
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Path { get; set; }
    }
}