using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConfigImageResourceSourceConverter : JsonConverter 
    {
        private readonly Type[] _types;
        
        public JobPlanConfigImageResourceSourceConverter()
        {
            _types = new[]
            {
                typeof(JobPlanConfigImageResourceSource),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resourceSource = (JobPlanConfigImageResourceSource) value;

            if (resourceSource != null)
            {
                writer.WriteStartObject();

                foreach (var kv in resourceSource)
                {
                    writer.WritePropertyName(kv.Key);
                    serializer.Serialize(writer, kv.Value);
                }

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var resourceSource = new JobPlanConfigImageResourceSource();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
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