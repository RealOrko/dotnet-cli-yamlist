using Newtonsoft.Json;

namespace yamlist.Modules.Json
{
    public class JsonConverter
    {
        public static string Format(string json)
        {
            dynamic jo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return Newtonsoft.Json.JsonConvert.SerializeObject(jo, Formatting.Indented);
        }
    }
}