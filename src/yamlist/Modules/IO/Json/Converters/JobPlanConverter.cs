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
    public class JobPlanConverter: JsonConverter
    {
        private readonly Type[] _types;

        public JobPlanConverter()
        {
            _types = new[] { typeof(JobPlan) };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobPlan = new JobPlan();

            if (reader.Value?.ToString()?.StartsWith("_anchor_call") != null)
            {
                var jobPlanAnchorCall = new JobPlanAnchorCall();
                jobPlanAnchorCall.AnchorCall = new AnchorCall();
                jobPlanAnchorCall.AnchorCall.Method = reader.Value.ToString();
                return jobPlanAnchorCall;
            }
            
            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name == "in_parallel")
                {
                    jobPlan.InParallel = new List<JobPlan>();
                    var jobPlanObjectType = typeof(List<JobPlan>);
                    var jobPlanJsonReader = new JsonTextReader(new StringReader(property.Value?.ToString()));
                    jobPlan.InParallel.AddRange(((List<JobPlan>)serializer.Deserialize(jobPlanJsonReader, jobPlanObjectType))!);
                    continue;
                }

                if (property.Name == "get")
                {
                    jobPlan.Get = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "put")
                {
                    jobPlan.Put = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "attempts")
                {
                    jobPlan.Attempts = Convert.ToInt32(property.Value.ToString());
                    continue;
                }
                
                if (property.Name == "trigger")
                {
                    jobPlan.Trigger = Convert.ToBoolean(property.Value.ToString());
                    continue;
                }
                
                if (property.Name == "passed")
                {
                    jobPlan.Passed = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "task")
                {
                    jobPlan.Task = property.Value?.ToString();
                    continue;
                }
                
                if (property.Name == "file")
                {
                    jobPlan.File = property.Value?.ToString();

                    if (jobPlan.File.Contains("app-metrics"))
                    {
                        "".ToString();
                    }
                    
                    continue;
                }
                
                if (property.Name == "input_mapping")
                {
                    jobPlan.File = property.Value?.ToString();
                    jobPlan.InputMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "output_mapping")
                {
                    jobPlan.File = property.Value?.ToString();
                    jobPlan.OutputMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(property.Value?.ToString());
                }

            }

            return jobPlan;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}