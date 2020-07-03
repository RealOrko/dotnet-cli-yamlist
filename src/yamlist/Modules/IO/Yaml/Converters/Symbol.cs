namespace yamlist.Modules.IO.Yaml.Converters
{
    public class Symbol
    {
        public string FindRegEx { get; }
        public string ReplaceWith { get; }

        public Symbol(string regEx, string replace)
        {
            FindRegEx = regEx;
            ReplaceWith = replace;
        }

        public override string ToString()
        {
            return $"{nameof(FindRegEx)}: {FindRegEx}, {nameof(ReplaceWith)}: {ReplaceWith}";
        }
    }
}