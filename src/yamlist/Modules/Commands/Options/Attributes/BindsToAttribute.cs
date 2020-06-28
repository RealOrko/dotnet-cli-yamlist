using System;

namespace yamlist.Modules.Commands.Options.Attributes
{
    public class BindsAttribute : Attribute
    {
        public BindsAttribute(Type arguments)
        {
            Arguments = arguments;
        }

        public Type Arguments { get; set; }
    }
}