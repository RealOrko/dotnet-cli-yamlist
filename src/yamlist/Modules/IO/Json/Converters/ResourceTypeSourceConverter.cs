using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class ResourceTypeSourceConverter : JsonConverter
    {
        private readonly Type[] _types;
        
        public ResourceTypeSourceConverter()
        {
            _types = new[] { typeof(ResourceTypeSource) };
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var resourceTypeSource = (ResourceTypeSource) value;

            if (resourceTypeSource != null)
            {
                writer.WriteStartObject();

                if (!string.IsNullOrEmpty(resourceTypeSource.Repository))
                {
                    writer.WritePropertyName("repository");
                    writer.WriteValue(resourceTypeSource.Repository);
                }
                
                if (!string.IsNullOrEmpty(resourceTypeSource.Tag))
                {
                    writer.WritePropertyName("tag");
                    writer.WriteValue(resourceTypeSource.Tag);
                }
                
                writer.WriteEndObject();
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var resourceTypeSource = new ResourceTypeSource();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "repository")
                {
                    resourceTypeSource.Repository = property.Value?.ToString();
                    continue;
                }
                
                if (property.Name == "tag")
                {
                    resourceTypeSource.Tag = property.Value?.ToString();
                    continue;
                }
            }

            return resourceTypeSource;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }    
    }
}