using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class ResourceTypeConverter : JsonConverter
    {
        private readonly Type[] _types;
        
        public ResourceTypeConverter()
        {
            _types = new[] { typeof(ResourceType) };
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var resourceType = (ResourceType) value;

            if (resourceType != null)
            {
                writer.WriteStartObject();

                if (!string.IsNullOrEmpty(resourceType.Name))
                {
                    writer.WritePropertyName("name");
                    writer.WriteValue(resourceType.Name);
                }
                
                if (!string.IsNullOrEmpty(resourceType.Type))
                {
                    writer.WritePropertyName("type");
                    writer.WriteValue(resourceType.Type);
                }
                
                if (resourceType.Source != null)
                {
                    writer.WritePropertyName("source");
                    serializer.Serialize(writer, resourceType.Source, typeof(ResourceTypeSource));
                }
                
                writer.WriteEndObject();
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var resourceType = new ResourceType();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "name")
                {
                    resourceType.Name = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "type")
                {
                    resourceType.Type = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "source")
                {
                    resourceType.Source = JsonConvert.DeserializeObject<ResourceTypeSource>(property.Value.ToString(), Converter.Settings);
                }
            }
            
            return resourceType;

        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }    
    }
}