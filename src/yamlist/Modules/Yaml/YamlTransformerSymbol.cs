namespace yamlist.Modules.Yaml
{
    public class YamlTransformerSymbol
    {
        public string ForwardRegEx { get; set; }
        public string ForwardReplacement { get; set; }
        public string ReverseRegEx { get; set; }
        public string ReverseReplacement { get; set; }

        public YamlTransformerSymbol(string forwardRegEx, string forwardReplacement, string reverseRegEx, string reverseReplacement)
        {
            ForwardRegEx = forwardRegEx;
            ForwardReplacement = forwardReplacement;
            ReverseRegEx = reverseRegEx;
            ReverseReplacement = reverseReplacement;
        }
    }
}