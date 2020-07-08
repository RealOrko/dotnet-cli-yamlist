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
                    typeof(JobPlan)
                }),
                Converters = new List<JsonConverter>()
                {
                    new JobConverter(),
                    new JobPlanConverter()
                }
            };
            
            return (Pipeline) JsonConvert.DeserializeObject(input, 
                typeof(Pipeline), 
                settings);
        }

        public static string ConcourseToJson(Pipeline input)
        {
            return JsonConvert.SerializeObject(input, Formatting.Indented);
        }

    }
}