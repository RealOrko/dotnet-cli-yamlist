using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace yamlist.Modules.Commands.Options
{
    public class ArgumentsParser
    {
        public static object Parse(Type commandType, Type argumentsType, string[] args, Func<string, string> getEnvironmentVariable = null)
        {
            var target = Activator.CreateInstance(argumentsType);
            var availableCommandArguments = new List<string>();
            foreach (var propertyInfo in target.GetType().GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttributes(typeof(ArgumentAttribute), false).Cast<ArgumentAttribute>().FirstOrDefault();
                if (attribute != null)
                {
                    availableCommandArguments.AddRange(new List<string> { attribute.LongName?.ToLowerInvariant(), attribute.ShortName?.ToLowerInvariant() });
                    if (ParseArgumentValue(attribute, target, propertyInfo, args))
                    {
                        continue;
                    }

                    var environment = GetEnvironmentVariableAndDefaultValue(attribute, getEnvironmentVariable);
                    if (environment.Any())
                    {
                        var value = environment.First();
                        propertyInfo.SetValue(target, value);
                    }
                }
            }

            ValidateArguments(commandType, argumentsType, args, availableCommandArguments);

            return target;
        }

        private static void ValidateArguments(Type commandType, Type argumentsType, string[] args, List<string> availableCommandArguments)
        {
            var paramArgs = args.Where(a => a.StartsWith("-"));
            var invalidArguments = paramArgs.Where(a => !availableCommandArguments.Contains(a.ToLowerInvariant()));
            if (invalidArguments.Any())
            {
                CommandParser.Info(commandType);
                
                throw new ArgumentException(
                    $"The following invalid arguments were passed {string.Join(",", invalidArguments)} for {argumentsType.Name}");
            }
        }

        public static T Parse<T>(Type commandType, string[] args, Func<string, string> getEnvironmentVariable = null) where T : new()
        {
            return (T) Parse(commandType, typeof(T), args, getEnvironmentVariable);
        }

        private static IEnumerable<string> GetEnvironmentVariableAndDefaultValue(ArgumentAttribute attribute, Func<string, string> getEnvironmentVariable = null)
        {
            var results = new List<string>();

            if (!string.IsNullOrWhiteSpace(attribute.EnvVar))
            {
                var item = (getEnvironmentVariable ?? Environment.GetEnvironmentVariable)(attribute.EnvVar);
                if (item != null)
                {
                    results.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(attribute.Default))
            {
                results.Add(attribute.Default);
            }

            return results;
        }

        private static bool ParseArgumentValue(ArgumentAttribute attribute, object target, PropertyInfo propertyInfo, string[] args)
        {
            for (var index = 0; index < args.Length; index++)
            {
                var arg = args[index];

                if (!arg.Equals(attribute.ShortName) && !arg.Equals(attribute.LongName))
                {
                    continue;
                }

                if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo.SetValue(target, true);
                    return true;
                }

                var value = index + 1 < args.Length ? args[index + 1] : null;
                propertyInfo.SetValue(target, value);
                return true;
            }

            return false;
        }
    }
}
