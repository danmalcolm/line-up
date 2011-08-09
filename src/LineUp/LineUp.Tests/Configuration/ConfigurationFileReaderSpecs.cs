using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using LineUp.Tests.Configuration.TestConfiguration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LineUp.Tests.Configuration.ConfigurationFileReaderSpecs
{
    [TestFixture]
    public class ConfigurationFileReaderSpecs
    {
        [Test]
        public void serialize_xml()
        {
            var config = DemoConfigBuilder.Build();
            var serializer = new XmlSerializer(typeof (Config));
            
            using(var writer = new StringWriter())
            {
                serializer.Serialize(writer, config);
                Console.WriteLine(writer.GetStringBuilder());
            }
        }

        [Test]
        public void serialize_json()
        {
            var config = DemoConfigBuilder.Build();
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            Console.WriteLine(json);
        }

        [Test]
        public void custom_text()
        {
            Console.WriteLine(@"
# Comments
 
.net    2.0     C:\Windows\Microsoft.NET\Framework\v2.0.50727\
.net    3.5     C:\Windows\Microsoft.NET\Framework\v3.5\
.net    4.0*    C:\Windows\Microsoft.NET\Framework\v4.0.30319\

sqltools 2008*  C:\Program....


");
        }
    }
}