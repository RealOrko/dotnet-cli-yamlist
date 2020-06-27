using System;
using yamlist.Modules.Commands.Options;

namespace yamlist.Modules.Commands.Routing
{
    public class CommandRouter
    {
        private readonly CommandFactory commandFactory;

        public CommandRouter()
        {
            commandFactory = new CommandFactory();
        }

        public CommandDispatcher Route(string[] args)
        {
            return Route(new CommandContext(args));
        }

        public CommandDispatcher Route(CommandContext argsContext)
        {
            var commandType = CommandParser.FindCommand(argsContext.Command);
            if (commandType != default(Type))
            {
                return commandFactory.CreateDispatcher(argsContext, commandType);
            }

            return null;
        }
    }
}