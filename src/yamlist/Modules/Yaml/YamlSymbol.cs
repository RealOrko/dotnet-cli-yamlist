namespace yamlist.Modules.Yaml
{
    public class YamlSymbol
    {
        public string RegEx { get; set; }
        public string Replacement { get; set; }

        public YamlSymbol(string regEx, string replacement)
        {
            RegEx = regEx;
            Replacement = replacement;
        }
    }
}