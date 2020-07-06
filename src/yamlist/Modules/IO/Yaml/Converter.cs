using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YamlDotNet.Serialization;

namespace yamlist.Modules.IO.Yaml
{
    public class Converter
    {
        public static string ToJson(string input)
        {
            var reader = new StringReader(input);
            var deserializer = new DeserializerBuilder().Build();
            var yaml = deserializer.Deserialize(reader);

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            var json = serializer.Serialize(yaml);

            return json;
        }

        public static string ToYaml(string input)
        {
            var expConverter = new ExpandoObjectConverter();
            dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(input, expConverter);

            var serializer = new Serializer();
            string yaml = serializer.Serialize(deserializedObject);
            
            return yaml;
        }
    }
}