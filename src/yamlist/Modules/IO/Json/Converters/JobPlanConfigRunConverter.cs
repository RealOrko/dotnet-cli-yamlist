using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConfigRunConverter : JsonConverter 
    {
        private readonly Type[] _types;
        
        public JobPlanConfigRunConverter()
        {
            _types = new[]
            {
                typeof(JobPlanConfigRun),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JobPlanConfigRun jpcr)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("path");
                writer.WriteValue(jpcr.Path);

                if (jpcr.Args != null && jpcr.Args.Count > 0)
                {
                    writer.WritePropertyName("args");
                    writer.WriteStartArray();

                    foreach (var arg in jpcr.Args)
                    {
                        writer.WriteValue(arg);
                    }

                    writer.WriteEndArray();
                }

                writer.WriteEndObject();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobPlanConfigRun = new JobPlanConfigRun();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "args")
                {
                    jobPlanConfigRun.Args = JsonConvert.DeserializeObject<List<string>>(property.Value.ToString());
                }
                if (property.Name == "path")
                {
                    jobPlanConfigRun.Path = property.Value.ToString();
                }
            }

            return jobPlanConfigRun;
        }
        
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}