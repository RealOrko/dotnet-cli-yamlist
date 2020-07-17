using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class PipelineConverter : JsonConverter
    {
        private readonly Type[] _types;
        
        public PipelineConverter()
        {
            _types = new[] { typeof(Pipeline) };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value is Pipeline p)
            {
                if (p.Anchors != null && p.Anchors.Count > 0)
                {
                    writer.WritePropertyName("anchors");
                    writer.WriteStartObject();
                    foreach (var a in p.Anchors)
                    {
                        writer.WritePropertyName(a.Key);
                        serializer.Serialize(writer, a.Value);
                    }
                    writer.WriteEndObject();
                }
                
                if (p.Groups != null && p.Groups.Count > 0)
                {
                    writer.WritePropertyName("groups");
                    writer.WriteStartArray();
                    foreach (var a in p.Groups)
                    {
                        serializer.Serialize(writer, a, typeof(Group));
                    }
                    writer.WriteEndArray();
                }
                
                if (p.Jobs != null && p.Jobs.Count > 0)
                {
                    writer.WritePropertyName("jobs");
                    writer.WriteStartArray();
                    foreach (var a in p.Jobs)
                    {
                        serializer.Serialize(writer, a, typeof(Job));
                    }
                    writer.WriteEndArray();
                }

                if (p.Resources != null && p.Resources.Count > 0)
                {
                    writer.WritePropertyName("resources");
                    writer.WriteStartArray();
                    foreach (var a in p.Resources)
                    {
                        serializer.Serialize(writer, a, typeof(Resource));
                    }
                    writer.WriteEndArray();
                }

                if (p.ResourceTypes != null && p.ResourceTypes.Count > 0)
                {
                    writer.WritePropertyName("resource_types");
                    writer.WriteStartArray();
                    foreach (var a in p.ResourceTypes)
                    {
                        serializer.Serialize(writer, a, typeof(ResourceType));
                    }
                    writer.WriteEndArray();
                }

            }
            
            writer.WriteEndObject();

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var pipeline = new Pipeline();
            
            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "anchors" || property.Name == "meta")
                {
                    pipeline.Anchors = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "groups")
                {
                    pipeline.Groups = JsonConvert.DeserializeObject<List<Group>>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "jobs")
                {
                    pipeline.Jobs = JsonConvert.DeserializeObject<List<Job>>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "resource_types")
                {
                    pipeline.ResourceTypes = JsonConvert.DeserializeObject<List<ResourceType>>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "resources")
                {
                    pipeline.Resources = JsonConvert.DeserializeObject<List<Resource>>(property.Value.ToString(), Converter.Settings);
                }
            }

            return pipeline;
        }
        
        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}