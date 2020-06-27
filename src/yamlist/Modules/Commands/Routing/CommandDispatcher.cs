using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace yamlist.Modules.Commands.Routing
{
    public class CommandDispatcher
    {
        public CommandDispatcher(object instance)
        {
            Instance = instance;
            ExecuteMethodInfo = Instance.GetType().GetMethod("Execute");
        }

        public CommandDispatcher(object instance, object[] parameters) : this(instance)
        {
            Parameters = parameters;
        }

        public CommandDispatcher(CommandContext context, object instance, object[] parameters) : this(instance,
            parameters)
        {
            Context = context;
        }

        public object Instance { get; }
        public object[] Parameters { get; }
        public Type CommandType => Instance.GetType();
        public CommandContext Context { get; set; }
        public MethodInfo ExecuteMethodInfo { get; }

        public int Execute()
        {
            try
            {
                if (Parameters == null) throw new Exception("Please supply parameters.");

                var returnCode = (int?) ExecuteMethodInfo.Invoke(Instance, Parameters);
                return returnCode.GetValueOrDefault(Environment.ExitCode);
            }
            catch (TargetInvocationException targetInvocationException)
            {
                ExceptionDispatchInfo.Capture(targetInvocationException.InnerException).Throw();
                return -1;
            }
        }
    }
}