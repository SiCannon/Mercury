using System;

namespace MusicBrainz.Helpers
{
    public static class ConsoleHelpers
    {
        public static void WriteToConsole(string format, ConsoleColor color, bool newLine, params object[] arg)
        {
            System.Console.ForegroundColor = color;
            System.Console.Write(format, arg);
            if (newLine)
                System.Console.WriteLine();
        }
    }
}
