using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConfigImageResourceConverter : JsonConverter 
    {
        private readonly Type[] _types;
        
        public JobPlanConfigImageResourceConverter()
        {
            _types = new[]
            {
                typeof(JobPlanConfigImageResource),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JobPlanConfigImageResource jpcr)
            {
                writer.WriteStartObject();

                if (!string.IsNullOrEmpty(jpcr.Type))
                {
                    writer.WritePropertyName("type");
                    writer.WriteValue(jpcr.Type);
                }

                if (jpcr.Source != null && jpcr.Source.Count > 0)
                {
                    writer.WritePropertyName("source");
                    serializer.Serialize(writer, jpcr.Source, typeof(JobPlanConfigImageResourceSource));
                }

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobPlanConfigImageResource = new JobPlanConfigImageResource();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "type")
                {
                    jobPlanConfigImageResource.Type = property.Value.ToString();
                }

                if (property.Name == "source")
                {
                    jobPlanConfigImageResource.Source = JsonConvert.DeserializeObject<JobPlanConfigImageResourceSource>(property.Value.ToString());
                }
            }

            return jobPlanConfigImageResource;
        }
        
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}