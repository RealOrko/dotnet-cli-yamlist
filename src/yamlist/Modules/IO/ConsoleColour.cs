using System;

namespace yamlist.Modules.IO
{
    public class ConsoleColour : IDisposable
    {
        private readonly ConsoleColor _preBackground;
        private readonly ConsoleColor _preForeground;

        public ConsoleColour(ConsoleColor? background = null, ConsoleColor? foreground = null)
        {
            _preBackground = System.Console.BackgroundColor;
            _preForeground = System.Console.ForegroundColor;

            System.Console.BackgroundColor = background.GetValueOrDefault(_preBackground);
            System.Console.ForegroundColor = foreground.GetValueOrDefault(_preForeground);
        }

        public void Dispose()
        {
            System.Console.BackgroundColor = _preBackground;
            System.Console.ForegroundColor = _preForeground;
        }
    }
}