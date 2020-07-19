using System;
using System.Collections.Generic;
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

                foreach (var kv in resourceTypeSource)
                {
                    writer.WritePropertyName(kv.Key);
                    serializer.Serialize(writer, kv.Value);
                }
                
                if (resourceTypeSource.Insecure_Registries != null && resourceTypeSource.Insecure_Registries.Count > 0)
                {
                    writer.WritePropertyName("insecure_registries");
                    writer.WriteStartArray();
                    foreach (var ignorePath in resourceTypeSource.Insecure_Registries)
                    {
                        writer.WriteValue(ignorePath);
                    }
                    writer.WriteEndArray();
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
                if (property.Name == "insecure_registries")
                {
                    resourceTypeSource.Insecure_Registries = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }

                resourceTypeSource.Add(property.Name, property.Value);
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