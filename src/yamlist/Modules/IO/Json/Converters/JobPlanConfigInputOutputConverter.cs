using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConfigInputOutputConverter : JsonConverter 
    {
        private readonly Type[] _types;
        
        public JobPlanConfigInputOutputConverter()
        {
            _types = new[]
            {
                typeof(JobPlanConfigInputOutput),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JobPlanConfigInputOutput jpci)
            {
                writer.WriteStartObject();

                if (!string.IsNullOrEmpty(jpci.Name))
                {
                    writer.WritePropertyName("name");
                    writer.WriteValue(jpci.Name);
                }

                if (!string.IsNullOrEmpty(jpci.Value))
                {
                    writer.WritePropertyName("value");
                    writer.WriteValue(jpci.Value);
                }

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobPlanConfigInputOutput = new JobPlanConfigInputOutput();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "name")
                {
                    jobPlanConfigInputOutput.Name = property.Value.ToString();
                }
                
                if (property.Name == "value")
                {
                    jobPlanConfigInputOutput.Value = property.Value.ToString();
                }
            }

            return jobPlanConfigInputOutput;
        }
        
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}