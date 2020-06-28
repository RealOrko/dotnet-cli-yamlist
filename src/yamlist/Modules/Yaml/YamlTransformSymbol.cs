namespace yamlist.Modules.Yaml
{
    public class YamlTransformSymbol
    {
        public YamlSymbol Forward { get; set; }
        public YamlSymbol Reverse { get; set; }
        
        public YamlTransformSymbol(string forwardRegEx, string forwardReplacement, string reverseRegEx, string reverseReplacement)
        {
            Forward = new YamlSymbol(forwardRegEx, forwardReplacement);
            Reverse = new YamlSymbol(reverseRegEx, reverseReplacement);
        }
    }
}