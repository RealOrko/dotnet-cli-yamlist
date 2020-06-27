using System;
using System.Linq;
using System.Text;

namespace yamlist.Modules.Commands.Options
{
    public class CommandParser
    {
        public static void InfoAll()
        {
            foreach (var commandType in typeof(CommandParser).Assembly.GetTypes().OrderBy(x => x.Name).Where(x =>
                x.FullName.EndsWith("Command") && x.GetCustomAttributes(typeof(BindsAttribute), false).Any()))
                Info(commandType);
        }

        public static string Info(Type commandType)
        {
            var builder = new StringBuilder();

            var bindsAttribute =
                (BindsAttribute) commandType.GetCustomAttributes(typeof(BindsAttribute), false).FirstOrDefault();

            if (bindsAttribute != null)
            {
                var argsType = bindsAttribute.Arguments;
                var commandAttribute = argsType.GetCustomAttributes(typeof(CommandAttribute), false)
                    .Cast<CommandAttribute>().FirstOrDefault();
                if (commandAttribute != null)
                {
                    builder.AppendLine();
                    var fullCommand = $"yi {commandAttribute.Command}";
                    builder.AppendLine(fullCommand);
                    builder.AppendLine(new string('-', fullCommand.Length));
                }

                builder.AppendLine();

                foreach (var propertyInfo in argsType.GetProperties())
                {
                    var argumentAttribute = propertyInfo.GetCustomAttributes(typeof(ArgumentAttribute), false)
                        .Cast<ArgumentAttribute>().FirstOrDefault();
                    if (argumentAttribute != null && argumentAttribute.IsVisible)
                    {
                        builder.AppendLine(string.Format($"   {argumentAttribute.DescribeArgument()}"));
                        builder.AppendLine(string.Format($"      {argumentAttribute.Help}"));
                        builder.AppendLine();
                    }
                }

                builder.AppendLine();
            }

            var help = builder.ToString();
            Console.WriteLine(help);

            return help;
        }

        public static string Info<T>() where T : new()
        {
            return Info(typeof(T));
        }

        public static bool IsBindable(Type commandType)
        {
            return commandType.GetCustomAttributes(typeof(BindsAttribute), false).Any();
        }

        public static string GetCommand(Type commandType)
        {
            var bindableAttribute =
                (BindsAttribute) commandType.GetCustomAttributes(typeof(BindsAttribute), false).FirstOrDefault();
            if (bindableAttribute != null)
            {
                var argumentType = bindableAttribute.Arguments;
                var commandAttribute =
                    (CommandAttribute) argumentType.GetCustomAttributes(typeof(CommandAttribute), false)
                        .FirstOrDefault();
                if (commandAttribute != null) return commandAttribute.Command;

                throw new Exception(
                    $"Cannot get install command, there is no CommandAttribute on {argumentType.FullName}.");
            }

            throw new Exception($"Cannot get install command, there is no BindsAttribute on {commandType.FullName}.");
        }

        public static object GetArguments(Type commandType, string[] args)
        {
            var bindableAttribute =
                (BindsAttribute) commandType.GetCustomAttributes(typeof(BindsAttribute), false).FirstOrDefault();
            if (bindableAttribute != null)
            {
                var argumentType = bindableAttribute.Arguments;
                return ArgumentsParser.Parse(commandType, argumentType, args);
            }

            throw new Exception(
                $"Cannot bind parameters for command, there is no BindsAttribute on {commandType.FullName}.");
        }

        public static T GetArguments<T>(string[] args)
        {
            return (T) GetArguments(typeof(T), args);
        }

        public static Type FindCommand(string commandAction)
        {
            foreach (var type in typeof(CommandParser).Assembly.GetTypes())
                if (type.FullName.EndsWith("Command") && IsBindable(type))
                    if (commandAction.ToLower().Equals(GetCommand(type)))
                        return type;

            return default;
        }
    }
}