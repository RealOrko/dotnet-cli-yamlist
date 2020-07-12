using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Converters
{
    public class ResourceConverter : JsonConverter
    {
        private readonly Type[] _types;
        
        public ResourceConverter()
        {
            _types = new[] { typeof(Resource) };
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var resource = (Resource) value;

            if (resource.SourceAnchorCall != null)
            {
                writer.WriteValue(resource.SourceAnchorCall.Method);
                return;
            }

            if (resource != null)
            {
                writer.WriteStartObject();
                
                if (!string.IsNullOrEmpty(resource.Name))
                {
                    writer.WritePropertyName("name");
                    writer.WriteValue(resource.Name);
                }

                if (!string.IsNullOrEmpty(resource.Type))
                {
                    writer.WritePropertyName("type");
                    writer.WriteValue(resource.Type);
                }

                if (!string.IsNullOrEmpty(resource.CheckEvery))
                {
                    writer.WritePropertyName("check_every");
                    writer.WriteValue(resource.CheckEvery);
                }

                if (resource.Source != null)
                {
                    writer.WritePropertyName("source");

                    if (resource.SourceAnchorCall != null)
                    {
                        writer.WriteValue(resource.SourceAnchorCall.Method);
                    }
                    else
                    {
                        serializer.Serialize(writer, resource.Source, typeof(ResourceSource));                    
                    }
                }

                writer.WriteEndObject();
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var resource = new Resource();
            
            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "name")
                {
                    resource.Name = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "type")
                {
                    resource.Type = property.Value.ToString();
                    continue;
                }

                if (property.Name == "check_every")
                {
                    resource.CheckEvery = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "source")
                {
                    if (property.Value == null) continue;
                    
                    if (property.Value.ToString().StartsWith("_anchor_call"))
                    {
                        resource.SourceAnchorCall = new AnchorCall();
                        resource.SourceAnchorCall.Method = property.Value.ToString();
                    }
                    else
                    {
                        resource.Source = JsonConvert.DeserializeObject<ResourceSource>(property.Value.ToString(), Converter.Settings);
                    }
                    continue;
                }
            }
            
            return resource;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }    
    }
}