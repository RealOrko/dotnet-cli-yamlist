using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class ResourceSourceConverter : JsonConverter
    {
        private readonly Type[] _types;
        
        public ResourceSourceConverter()
        {
            _types = new[] { typeof(ResourceSource) };
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var resourceSource = (ResourceSource) value;

            if (resourceSource != null)
            {
                writer.WriteStartObject();

                foreach (var kv in resourceSource)
                {
                    writer.WritePropertyName(kv.Key);
                    serializer.Serialize(writer, kv.Value);
                }
                
                if (resourceSource.IgnorePaths != null && resourceSource.IgnorePaths.Count > 0)
                {
                    writer.WritePropertyName("ignore_paths");
                    writer.WriteStartArray();
                    foreach (var ignorePath in resourceSource.IgnorePaths)
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
            var resourceSource = new ResourceSource();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "ignore_paths")
                {
                    resourceSource.IgnorePaths = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }

                resourceSource.Add(property.Name, property.Value);
            }
            
            return resourceSource;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }    
    }
}