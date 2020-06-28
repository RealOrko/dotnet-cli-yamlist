using Newtonsoft.Json;

namespace yamlist.Modules.IO.Json
{
    public class Converter
    {
        public static string Format(string json)
        {
            dynamic jo = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(jo, Formatting.Indented);
        }
    }
}