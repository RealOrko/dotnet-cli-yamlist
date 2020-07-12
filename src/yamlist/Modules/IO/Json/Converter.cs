using System.Collections.Generic;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Converters;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Resolvers;

namespace yamlist.Modules.IO.Json
{
    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new DisableContractResolver(new[]
            {
                typeof(Job),
                typeof(JobPlan),
                typeof(JobPlanEnsure),
                typeof(JobPlanConfig),
                typeof(JobPlanConfigRun),
                typeof(JobPlanConfigInputOutput),
                typeof(Resource),
                typeof(ResourceSource),
                typeof(ResourceType),
                typeof(ResourceTypeSource),
                typeof(Pipeline),
            }),
            Converters = new List<JsonConverter>()
            {
                new JobConverter(),
                new JobPlanConverter(),
                new JobPlanConfigConverter(),
                new JobPlanConfigRunConverter(),
                new JobPlanConfigInputOutputConverter(),
                new ResourceConverter(),
                new ResourceSourceConverter(),
                new ResourceTypeConverter(),
                new ResourceTypeSourceConverter(),
                new PipelineConverter(),
            }
        };
        
        public static string Format(string json)
        {
            dynamic jo = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jo, Formatting.Indented);
        }
        
        public static Pipeline JsonToConcourse(string input)
        {
            return (Pipeline) JsonConvert.DeserializeObject(input, typeof(Pipeline), Settings);
        }

        public static string ConcourseToJson(Pipeline input)
        {
            return JsonConvert.SerializeObject(input, Formatting.Indented, Settings);
        }

    }
}