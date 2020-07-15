using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Model.Meta;

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

                if (!string.IsNullOrEmpty(jpc.Platform))
                {
                    writer.WritePropertyName("platform");
                    writer.WriteValue(jpc.Platform);
                }

                if (jpc.Inputs != null && jpc.Inputs.Count > 0)
                {
                    writer.WritePropertyName("inputs");

                    writer.WriteStartArray();
                    foreach (var configInput in jpc.Inputs)
                    {
                        serializer.Serialize(writer, configInput, typeof(JobPlanConfigInputOutput));
                    }
                    writer.WriteEndArray();
                }

                if (jpc.Outputs != null && jpc.Outputs.Count > 0)
                {
                    writer.WritePropertyName("outputs");

                    writer.WriteStartArray();
                    foreach (var configOutput in jpc.Outputs)
                    {
                        serializer.Serialize(writer, configOutput, typeof(JobPlanConfigInputOutput));
                    }
                    writer.WriteEndArray();
                }

                if (jpc.Params != null && jpc.Params.Count > 0)
                {
                    if (jpc.ParamsAnchorDeclaration != null)
                    {
                        writer.WritePropertyName(jpc.ParamsAnchorDeclaration.Method);
                    }
                    else
                    {
                        writer.WritePropertyName("params");
                    }

                    serializer.Serialize(writer, jpc.Params);
                }

                if (jpc.Run != null)
                {
                    writer.WritePropertyName("run");
                    serializer.Serialize(writer, jpc.Run, typeof(JobPlanConfigRun));
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
                if (property.Name == "platform")
                {
                    jobPlanConfig.Platform = property.Value.ToString();
                }

                if (property.Name == "inputs")
                {
                    jobPlanConfig.Inputs = JsonConvert.DeserializeObject<List<JobPlanConfigInputOutput>>(property.Value.ToString(), Converter.Settings);
                }

                if (property.Name == "outputs") 
                {
                    jobPlanConfig.Outputs = JsonConvert.DeserializeObject<List<JobPlanConfigInputOutput>>(property.Value.ToString(), Converter.Settings);
                }

                if (property.Name == "run")
                {
                    jobPlanConfig.Run = JsonConvert.DeserializeObject<JobPlanConfigRun>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "params")
                {
                    jobPlanConfig.Params = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name.StartsWith("params_anchor_decl_"))
                {
                    jobPlanConfig.ParamsAnchorDeclaration = new AnchorDeclaration();
                    jobPlanConfig.ParamsAnchorDeclaration.Method = property.Name;
                    jobPlanConfig.Params = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value?.ToString());
                }

            }

            return jobPlanConfig;
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}