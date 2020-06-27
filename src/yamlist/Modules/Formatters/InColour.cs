using System;

namespace yamlist.Modules.Formatters
{
    public class InColour : IDisposable
    {
        private readonly ConsoleColor _preBackground;
        private readonly ConsoleColor _preForeground;

        public InColour(ConsoleColor? background = null, ConsoleColor? foreground = null)
        {
            _preBackground = Console.BackgroundColor;
            _preForeground = Console.ForegroundColor;

            Console.BackgroundColor = background.GetValueOrDefault(_preBackground);
            Console.ForegroundColor = foreground.GetValueOrDefault(_preForeground);
        }

        public void Dispose()
        {
            Console.BackgroundColor = _preBackground;
            Console.ForegroundColor = _preForeground;
        }
    }}