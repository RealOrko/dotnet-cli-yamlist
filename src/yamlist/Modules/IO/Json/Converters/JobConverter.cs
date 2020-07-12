using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobConverter: JsonConverter
    {
        private readonly Type[] _types;
        private readonly JobPlanConverter jobPlanConverter;

        public JobConverter()
        {
            _types = new[] { typeof(Job) };
            jobPlanConverter = new JobPlanConverter();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value is Job j)
            {
                if (!string.IsNullOrEmpty(j.Name))
                {
                    writer.WritePropertyName("name");
                    writer.WriteValue(j.Name);
                }

                if (j.MergeCall != null)
                {
                    writer.WritePropertyName(j.MergeCall.Name);
                    writer.WriteValue(j.MergeCall.Method);
                }

                if (j.Serial)
                {
                    writer.WritePropertyName("serial");
                    writer.WriteValue(true);
                }
                
                if (j.SerialGroups != null && j.SerialGroups.Count > 0)
                {
                    writer.WritePropertyName("serial_groups");
                    writer.WriteStartArray();
                    foreach (var sg in j.SerialGroups)
                    {
                        writer.WriteValue(sg);
                    }
                    writer.WriteEndArray();
                }
                
                if (j.Plan != null && j.Plan.Count > 0)
                {
                    writer.WritePropertyName("plan");
                    writer.WriteStartArray();
                    foreach (var p in j.Plan)
                    {
                        serializer.Serialize(writer, p, typeof(JobPlan));
                    }
                    writer.WriteEndArray();
                }
            }
            
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var job = new Job();

            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "name")
                {
                    job.Name = property.Value?.ToString();
                    continue;
                }

                if (property.Name == "serial")
                {
                    job.Serial = Convert.ToBoolean(property.Value?.ToString());
                    continue;
                }

                if (property.Name == "serial_groups")
                {
                    job.SerialGroups = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "plan")
                {
                    job.Plan = JsonConvert.DeserializeObject<List<JobPlan>>(property.Value.ToString(), Converter.Settings);
                    continue;
                }

                if (property.Name.StartsWith("_merge"))
                {
                    var mergeCall = new MergeCall();
                    mergeCall.Name = property.Name;
                    mergeCall.Method = property.Value?.ToString();
                    job.MergeCall = mergeCall;
                }
            }

            return job;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}