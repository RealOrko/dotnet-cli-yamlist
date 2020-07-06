using System;
using System.Collections.Generic;
using System.IO;
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
            throw new NotImplementedException();
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
                    job.Plan = new List<JobPlan>();
                    var jobPlanObjectType = typeof(List<JobPlan>);
                    var jobPlanJsonReader = new JsonTextReader(new StringReader(property.Value?.ToString()));
                    job.Plan.AddRange(((List<JobPlan>)serializer.Deserialize(jobPlanJsonReader, jobPlanObjectType))!);
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