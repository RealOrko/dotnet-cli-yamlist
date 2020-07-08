using System.Collections.Generic;
using Newtonsoft.Json;
using yamlist.Modules.IO.Json.Converters;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Resolvers;

namespace yamlist.Modules.IO.Json
{
    public class Converter
    {
        public static string Format(string json)
        {
            dynamic jo = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jo, Formatting.Indented);
        }
        
        public static Pipeline JsonToConcourse(string input)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DisableContractResolver(new[]
                {
                    typeof(Job),
                    typeof(JobPlan),
                    typeof(Resource),
                    typeof(ResourceSource),
                    typeof(ResourceType),
                    typeof(ResourceTypeSource),
                }),
                Converters = new List<JsonConverter>()
                {
                    new JobConverter(),
                    new JobPlanConverter(),
                    new ResourceConverter(),
                    new ResourceSourceConverter(),
                    new ResourceTypeConverter(),
                    new ResourceTypeSourceConverter()
                }
            };
            
            return (Pipeline) JsonConvert.DeserializeObject(input, 
                typeof(Pipeline), 
                settings);
        }

        public static string ConcourseToJson(Pipeline input)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DisableContractResolver(new[]
                {
                    typeof(Job),
                    typeof(JobPlan),
                    typeof(Resource),
                    typeof(ResourceSource),
                    typeof(ResourceType),
                    typeof(ResourceTypeSource),
                }),
                Converters = new List<JsonConverter>()
                {
                    new JobConverter(),
                    new JobPlanConverter(),
                    new ResourceConverter(),
                    new ResourceSourceConverter(),
                    new ResourceTypeConverter(),
                    new ResourceTypeSourceConverter()
                }
            };
            
            return JsonConvert.SerializeObject(input, Formatting.Indented, settings);
        }

    }
}