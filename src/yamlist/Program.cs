using System;
using yamlist.Modules.Commands.Options;
using yamlist.Modules.Commands.Routing;
using yamlist.Modules.Formatters;
using yamlist.Modules.Versioning;

namespace yamlist
{
    class Program
    {
        private static CommandRouter _router = new CommandRouter();
        
        static int Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help" || args[0] == "/?" || args[0] == "?")
            {
                PrintUsage();
                return 0;
            }

            var result = 0;

            try
            {
                var dispatcher = _router.Route(args);
                if (dispatcher == null)
                {
                    PrintUsage();
                    return 1;
                }

                result = dispatcher.Execute();

            }
            catch (Exception err)
            {
                using (new InColour(ConsoleColor.Red, ConsoleColor.Black))
                {
                    Console.WriteLine(err.ToString());
                    Console.WriteLine();
                    Console.WriteLine(err.InnerException?.Message);
                    Console.WriteLine("Exiting with code -1");
                    result = -1;
                }
                
                Console.WriteLine("\r\n");
            }

            return result;
        }


        static void PrintUsage()
        {
            Console.WriteLine($"yi v{Info.GetVersion()} by realorko \r\n");
            CommandParser.InfoAll();
        }
    }
}
