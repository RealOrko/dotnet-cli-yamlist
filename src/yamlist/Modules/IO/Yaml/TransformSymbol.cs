namespace yamlist.Modules.IO.Yaml
{
    public class TransformSymbol
    {
        public Symbol Forward { get; }
        public Symbol Reverse { get; }
        
        public TransformSymbol(string forwardRegEx, string forwardReplacement, string reverseRegEx, string reverseReplacement)
        {
            Forward = new Symbol(forwardRegEx, forwardReplacement);
            Reverse = new Symbol(reverseRegEx, reverseReplacement);
        }

        public override string ToString()
        {
            return $"{nameof(Forward)}: {Forward}, {nameof(Reverse)}: {Reverse}";
        }
    }
}