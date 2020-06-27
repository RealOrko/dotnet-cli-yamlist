using System;

namespace yamlist.Modules.Commands.Options
{
    public class ArgumentAttribute : Attribute
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Help { get; set; }
        public string Default { get; set; }
        public bool IsVisible { get; set; } = true;
        public string EnvVar { get; set; }

        public string DescribeArgument()
        {
            return EnvVar != null
                ? $"{LongName}, {ShortName} or {EnvVar} env var"
                : $"{LongName} or {ShortName}";
        }
    }
}