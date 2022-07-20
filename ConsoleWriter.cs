using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuCreatorV2
{
    public class ConsoleWriter
    {
        /// <summary>
        /// Delete a line in the terminal
        /// </summary>
        /// <param name="top">Number of spaces from the top of the terminal</param>
        public static void ClearLine(int top)
        {
            int prevTop = Console.CursorTop;
            int prevLeft = Console.CursorLeft;

            Console.SetCursorPosition(0, top);

            Console.Write(new String(' ', Console.BufferWidth));

            Console.CursorTop = prevTop;
            Console.CursorLeft = prevLeft;
        }

        /// <summary>
        /// Write something in a certain color
        /// </summary>
        /// <param name="text">The string to write</param>
        /// <param name="fg">Foreground color</param>
        /// <param name="bg">Background color</param>
        /// <param name="reset">True if the color resets to the one before the call of the function</param>
        public static void WriteColor(string text, ConsoleColor fg, ConsoleColor bg, bool reset = true, bool line=false)
        {
            // Salvataggio colori precenti
            ConsoleColor prevFg = Console.ForegroundColor;
            ConsoleColor prevBg = Console.BackgroundColor;

            // Impostazione colori nuovi
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;

            // Scrittura
            if (line)
            {
                int spaceLeft = Console.CursorLeft;
                int spaceRight = (Console.BufferWidth - text.Length - Console.CursorLeft);

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', spaceLeft));

                Console.Write(text);

                Console.Write(new string(' ', spaceRight));

            }
            else
            {
                Console.Write(text);
            }

            if (reset)
            {
                Console.ForegroundColor = prevFg;
                Console.BackgroundColor = prevBg;
            }
        }
    }
}
