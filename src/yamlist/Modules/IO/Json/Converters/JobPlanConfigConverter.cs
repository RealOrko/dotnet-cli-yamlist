using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConfigConverter : JsonConverter
    {
        private readonly Type[] _types;

        public JobPlanConfigConverter()
        {
            _types = new[]
            {
                typeof(JobPlanConfig)
            };
        }

        public override bool CanRead => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JobPlanConfig jpc)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("platform");
                writer.WriteValue(jpc.Platform);

                if (jpc.Inputs != null && jpc.Inputs.Count > 0)
                {
                    writer.WritePropertyName("inputs");

                    writer.WriteStartArray();

                    foreach (var configInput in jpc.Inputs)
                    {
                        JsonConvert.SerializeObject(configInput, Converter.Settings);
                    }

                    writer.WriteEndArray();
                }

                if (jpc.Outputs != null && jpc.Outputs.Count > 0)
                {
                    writer.WritePropertyName("outputs");

                    writer.WriteStartArray();

                    foreach (var configOutput in jpc.Outputs)
                    {
                        JsonConvert.SerializeObject(configOutput, Converter.Settings);
                    }

                    writer.WriteEndArray();
                }

                if (jpc.Run != null)
                {
                    writer.WritePropertyName("run");
                    JsonConvert.SerializeObject(jpc.Run, Converter.Settings);
                }

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,             JsonSerializer serializer)
        {
            var jobPlanConfig = new JobPlanConfig();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "platform") jobPlanConfig.Platform = property.Value.ToString();

                if (property.Name == "inputs")
                    jobPlanConfig.Inputs = JsonConvert.DeserializeObject<List<JobPlanConfigInputOutput>>(property.Value.ToString(), Converter.Settings);

                if (property.Name == "outputs")
                    jobPlanConfig.Outputs = JsonConvert.DeserializeObject<List<JobPlanConfigInputOutput>>(property.Value.ToString(), Converter.Settings);

                if (property.Name == "run")
                    jobPlanConfig.Run = JsonConvert.DeserializeObject<JobPlanConfigRun>(property.Value.ToString(), Converter.Settings);
            }

            return jobPlanConfig;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}