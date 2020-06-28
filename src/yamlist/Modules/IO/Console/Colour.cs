using System;

namespace yamlist.Modules.IO.Console
{
    public class Colour : IDisposable
    {
        private readonly ConsoleColor _preBackground;
        private readonly ConsoleColor _preForeground;

        public Colour(ConsoleColor? background = null, ConsoleColor? foreground = null)
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