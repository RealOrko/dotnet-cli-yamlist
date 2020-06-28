namespace yamlist.Modules.Yaml
{
    public static class TransformSymbols
    {
        public static readonly TransformSymbol AnchorDeclaration = new TransformSymbol(
            @"(:\s*\&)", 
            "_anchor_decl_", 
            @"(_anchor_decl_)", 
            ": &");

        public static readonly TransformSymbol MergeAnchorDeclaration = new TransformSymbol(
            @"(<<\s*:\s*\&)", 
            "_merge_anchor_decl_{0}_", 
            @"(_merge_anchor_decl_\d*_)", 
            "<<: &");

        public static readonly TransformSymbol AnchorCall = new TransformSymbol(
            @"(?=[\ \t])(\s*\*)", 
            " _anchor_call_{0}_", 
            @"(_anchor_call_\d*_)", 
            "*");

        public static readonly TransformSymbol MergeAnchorCall = new TransformSymbol(
            @"(<<\s*:\s*\*)", 
            "_merge_{0}_: _call_anchor_{0}_", 
            @"(_merge_\d*_:\s*_call_anchor_\d*_)", 
            "<<: *");

    }
}