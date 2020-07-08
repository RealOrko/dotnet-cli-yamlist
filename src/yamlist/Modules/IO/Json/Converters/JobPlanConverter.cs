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
            if (value is JobPlanAnchorCall jpa)
            {
                return;
            }

            if (value is JobPlan jp)
            {
                writer.WriteStartObject();
                
                if (jp.InParallel != null && jp.InParallel.Count > 0)
                {
                    writer.WritePropertyName("in_parallel");
                    writer.WriteStartArray();
                    foreach (var jpp in jp.InParallel)
                    {
                        writer.WriteStartObject();
                        WriteJobPlan(writer, jpp);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                }
                
                WriteJobPlan(writer, jp);

                writer.WriteEndObject();
            }
        }

        private static void WriteJobPlan(JsonWriter writer, JobPlan jp)
        {
            if (jp.Do != null && jp.Do.Count > 0)
            {
                writer.WritePropertyName("do");
                writer.WriteStartArray();
                foreach (var jpp in jp.Do)
                {
                    writer.WriteStartObject();
                    WriteJobPlan(writer, jpp);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
                
            if (!string.IsNullOrEmpty(jp.SetPipeline))
            {
                writer.WritePropertyName("set_pipeline");
                writer.WriteValue(jp.SetPipeline);
            }

            if (!string.IsNullOrEmpty(jp.Get))
            {
                writer.WritePropertyName("get");
                writer.WriteValue(jp.Get);
            }

            if (!string.IsNullOrEmpty(jp.Put))
            {
                writer.WritePropertyName("put");
                writer.WriteValue(jp.Put);
            }

            if (jp.Trigger)
            {
                writer.WritePropertyName("trigger");
                writer.WriteValue(true);
            }

            if (!string.IsNullOrEmpty(jp.Task))
            {
                writer.WritePropertyName("task");
                writer.WriteValue(jp.Task);
            }

            if (!string.IsNullOrEmpty(jp.Image))
            {
                writer.WritePropertyName("image");
                writer.WriteValue(jp.Image);
            }

            if (!string.IsNullOrEmpty(jp.File))
            {
                writer.WritePropertyName("file");
                writer.WriteValue(jp.File);
            }

            if (jp.VarFiles != null && jp.VarFiles.Count > 0)
            {
                writer.WritePropertyName("var_files");
                writer.WriteStartArray();

                foreach (var varFile in jp.VarFiles)
                {
                    writer.WriteValue(varFile);
                }

                writer.WriteEndArray();
            }

            if (jp.Attempts > 0)
            {
                writer.WritePropertyName("attempts");
                writer.WriteValue(jp.Attempts);
            }

            if (jp.Passed != null && jp.Passed.Count > 0)
            {
                writer.WritePropertyName("passed");
                writer.WriteStartArray();

                foreach (var passed in jp.Passed)
                {
                    writer.WriteValue(passed);
                }

                writer.WriteEndArray();
            }

            if (jp.Params != null && jp.Params.Count > 0)
            {
                writer.WritePropertyName("params");
                writer.WriteStartObject();

                foreach (var kv in jp.Params)
                {
                    writer.WritePropertyName(kv.Key);
                    writer.WriteValue(kv.Value);
                }

                writer.WriteEndObject();
            }

            if (jp.InputMapping != null && jp.InputMapping.Count > 0)
            {
                writer.WritePropertyName("input_mapping");
                writer.WriteStartObject();

                foreach (var kv in jp.InputMapping)
                {
                    writer.WritePropertyName(kv.Key);
                    writer.WriteValue(kv.Value);
                }

                writer.WriteEndObject();
            }

            if (jp.OutputMapping != null && jp.OutputMapping.Count > 0)
            {
                writer.WritePropertyName("output_mapping");
                writer.WriteStartObject();

                foreach (var kv in jp.OutputMapping)
                {
                    writer.WritePropertyName(kv.Key);
                    writer.WriteValue(kv.Value);
                }

                writer.WriteEndObject();
            }
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

                if (property.Name == "do")
                {
                    jobPlan.Do = new List<JobPlan>();
                    var jobPlanObjectType = typeof(List<JobPlan>);
                    var jobPlanJsonReader = new JsonTextReader(new StringReader(property.Value?.ToString()));
                    jobPlan.Do.AddRange(((List<JobPlan>)serializer.Deserialize(jobPlanJsonReader, jobPlanObjectType))!);
                    continue;
                }

                if (property.Name == "set_pipeline")
                {
                    jobPlan.SetPipeline = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "var_files")
                {
                    jobPlan.VarFiles = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
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
                
                if (property.Name == "image")
                {
                    jobPlan.Image = property.Value?.ToString();
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
                    jobPlan.InputMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "output_mapping")
                {
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