namespace yamlist.Modules.IO.Yaml.Converters
{
    public static class SymbolMapper
    {
        public static readonly SymbolMap AnchorDeclaration = new SymbolMap(
            @"(:\s*\&)", 
            "_anchor_decl_", 
            @"(_anchor_decl_)", 
            ": &");

        public static readonly SymbolMap AnchorListDeclaration = new SymbolMap(
            @"(-\s*\&)", 
            "- _anchor_list_decl_", 
            @"(- _anchor_list_decl_)", 
            "- &");

        public static readonly SymbolMap MergeAnchorDeclaration = new SymbolMap(
            @"(<<\s*:\s*\&)", 
            "_merge_anchor_decl_{0}_", 
            @"(_merge_anchor_decl_\d*_)", 
            "<<: &");

        public static readonly SymbolMap AnchorCall = new SymbolMap(
            @"(?=[\ \t])(\s*\*)", 
            " _anchor_call_{0}_", 
            @"(_anchor_call_\d*_)", 
            "*");

        public static readonly SymbolMap MergeAnchorCall = new SymbolMap(
            @"(<<\s*:\s*\*)", 
            "_merge_{0}_: _call_anchor_{0}_", 
            @"(_merge_\d*_:\s*_call_anchor_\d*_)", 
            "<<: *");

    }
}