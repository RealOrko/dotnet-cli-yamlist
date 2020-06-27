using System;
using System.Linq;
using yamlist.Modules.Commands.Options;

namespace yamlist.Modules.Commands.Routing
{
    public class CommandFactory
    {
        public CommandDispatcher CreateDispatcher(CommandContext context, Type commandType)
        {
            var arguments = CommandParser.GetArguments(commandType, context.Arguments);
            var command = CreateCommand(commandType, context);

            var dispatcher = new CommandDispatcher(context, command, new[] {arguments});
            dispatcher.Context = context;
            return dispatcher;
        }

        private object CreateCommand(Type selectedCommandType, CommandContext context)
        {
            var ctor = selectedCommandType.GetConstructors().First();
            var instance = ctor.Invoke(new[] {context});
            return instance;
        }
    }
}