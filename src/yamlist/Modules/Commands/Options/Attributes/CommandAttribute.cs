using System;

namespace yamlist.Modules.Commands.Options.Attributes
{
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(string command)
        {
            Command = command;
        }

        public string Command { get; set; } = string.Empty;
    }
}