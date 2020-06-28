using System;
using System.Linq;
using yamlist.Modules.Commands.Options;

namespace yamlist.Modules.Commands
{
    public class Factory
    {
        public Dispatcher CreateDispatcher(Context context, Type commandType)
        {
            var arguments = CommandParser.GetArguments(commandType, context.Arguments);
            var command = CreateCommand(commandType, context);

            var dispatcher = new Dispatcher(context, command, new[] {arguments});
            dispatcher.Context = context;
            return dispatcher;
        }

        private object CreateCommand(Type selectedCommandType, Context context)
        {
            var ctor = selectedCommandType.GetConstructors().First();
            var instance = ctor.Invoke(new[] {context});
            return instance;
        }
    }
}