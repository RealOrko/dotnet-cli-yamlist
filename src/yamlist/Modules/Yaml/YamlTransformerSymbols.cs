namespace yamlist.Modules.Yaml
{
    public static class YamlTransformerSymbols
    {
        public static readonly YamlTransformSymbol AnchorDecl = new YamlTransformSymbol(
            @"(:\s*\&)", 
            "_anchor_decl_", 
            @"(_anchor_decl_)", 
            ": &");

        public static readonly YamlTransformSymbol MergeAnchorDecl = new YamlTransformSymbol(
            @"(<<\s*:\s*\&)", 
            "_merge_anchor_decl_{0}_", 
            @"(_merge_anchor_decl_\d*_)", 
            "<<: &");

        public static readonly YamlTransformSymbol AnchorCall = new YamlTransformSymbol(
            @"(?=[\ \t])(\s*\*)", 
            " _anchor_call_{0}_", 
            @"(_anchor_call_\d*_)", 
            "*");

        public static readonly YamlTransformSymbol MergeAnchorCall = new YamlTransformSymbol(
            @"(<<\s*:\s*\*)", 
            "_merge_{0}_: _call_anchor_{0}_", 
            @"(_merge_\d*_:\s*_call_anchor_\d*_)", 
            "<<: *");

    }
}