using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json
{
    public class Converter
    {
        public static string Format(string json)
        {
            dynamic jo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            return Newtonsoft.Json.JsonConvert.SerializeObject(jo, Formatting.Indented);
        }
    }
}