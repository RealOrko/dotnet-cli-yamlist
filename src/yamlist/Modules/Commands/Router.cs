using yamlist.Modules.Commands.Options;

namespace yamlist.Modules.Commands
{
    public class Router
    {
        private readonly Factory commandFactory;

        public Router()
        {
            commandFactory = new Factory();
        }

        public Dispatcher Route(string[] args)
        {
            return Route(new Context(args));
        }

        public Dispatcher Route(Context argsContext)
        {
            var commandType = CommandParser.FindCommand(argsContext.Command);
            if (commandType != default) return commandFactory.CreateDispatcher(argsContext, commandType);

            return null;
        }
    }
}