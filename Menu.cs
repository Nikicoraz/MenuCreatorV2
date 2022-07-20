namespace MenuCreatorV2
{
    public class Menu
    {
        /// <summary>
        /// Decide how to align the menu
        /// </summary>
        public enum Align
        {
            LEFT,
            CENTER,
            RIGHT
        }
        /// <summary>
        /// Function to align the cursor to write the menu entries
        /// </summary>
        /// <param name="option">The option that will be written (it's required for it's length)</param>
        /// <param name="x">The left position of the cursor</param>
        /// <param name="y">The top position of the cursor</param>
        /// <param name="optionNumber">The number of the option in the list</param>
        /// <param name="alignment">The alignment of the option</param>
        private static void AlignCursor(string option, int x, int y, int optionNumber, Align alignment)
        {
            switch (alignment)
            {
                case Align.LEFT:
                    Console.SetCursorPosition(x, y + optionNumber);
                    break;
                case Align.CENTER:
                    Console.SetCursorPosition((Console.BufferWidth - option.Length) / 2, y + optionNumber);
                    break;
                case Align.RIGHT:
                    Console.SetCursorPosition(Console.BufferWidth - option.Length, y + optionNumber);
                    break;
            }
        }

        /// <summary>
        /// Function that manages the selection in the menu
        /// </summary>
        /// <param name="options">The list of options</param>
        /// <param name="x">The left position of the menu</param>
        /// <param name="y">The top position of the menu</param>
        /// <param name="alignment">How the menu is aligned</param>
        /// <param name="fg">The foreground color of the menu options</param>
        /// <param name="bg">The background color of the menu options</param>
        /// <param name="selectedFg">The foreground color of the selected menu option</param>
        /// <param name="selectedBg">The background color of the selected menu option</param>
        /// <returns>The selected option</returns>
        private static int MenuSelection(string[] options, int x, int y, Align alignment, ConsoleColor fg, ConsoleColor bg,
            ConsoleColor selectedFg, ConsoleColor selectedBg)
        {
            // Selezione prima opzione
            ConsoleWriter.ClearLine(y);
            AlignCursor(options[0], x, y, 0, alignment);
            ConsoleWriter.WriteColor(options[0], selectedFg, selectedBg, line:true);

            int currentOption = 0;
            int oldOption = 0;


            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    // Navigazione delle opzioni
                    case ConsoleKey.UpArrow:
                        currentOption = currentOption > 0 ? currentOption - 1 : options.Length - 1; 
                        break;
                    case ConsoleKey.DownArrow:
                        currentOption = currentOption < options.Length - 1 ? currentOption + 1 : 0;
                        break;
                }

                // Riscrivi l'opzione precedente con le impostazioni di default
                ConsoleWriter.ClearLine(y + oldOption);
                AlignCursor(options[oldOption], x, y, oldOption, alignment);
                ConsoleWriter.WriteColor(options[oldOption], fg, bg, line:true);

                // The new option
                ConsoleWriter.ClearLine(y + currentOption);
                AlignCursor(options[currentOption], x, y, currentOption, alignment);
                ConsoleWriter.WriteColor(options[currentOption], selectedFg, selectedBg, line: true);

                oldOption = currentOption;

            } while (key.Key != ConsoleKey.Enter);
            return currentOption;
        }

        /// <summary>
        /// Create an interactive menu
        /// </summary>
        /// <param name="options">The selectable options of the menu (ex. Format, Delete, Write...)</param>
        /// <param name="x">Number of spaces from the left side of the terminal</param>
        /// <param name="y">Number of spaces from the top of the terminal</param>
        /// <param name="alignment">The alignment of the terminal</param>
        /// <param name="fg">The foreground color of the menu options</param>
        /// <param name="bg">The background color of the menu options</param>
        /// <param name="selectedFg">The foreground color of the selected menu option</param>
        /// <param name="selectedBg">The background color of the selected menu option</param>
        /// <param name="reset">Delete the menu after selecting an option</param>
        /// <returns>The index of the selected option</returns>
        public static int CreateMenu(string[] options, int x = -1, int y = -1, Align alignment = Align.LEFT,
            ConsoleColor fg = ConsoleColor.Gray, ConsoleColor bg = ConsoleColor.Black,
            ConsoleColor selectedFg = ConsoleColor.Black, ConsoleColor selectedBg = ConsoleColor.Gray, bool reset = true)
        {
            int initX = Console.CursorLeft;
            int initY = Console.CursorTop;
            // Setup
            Console.CursorVisible = false;

            x = x != -1 ? x : Console.CursorLeft;
            y = y != -1 ? y : Console.CursorTop;


            for (int i = 0; i < options.Length; i++)
            {
                AlignCursor(options[i], x, y, i, alignment);
                ConsoleWriter.WriteColor(options[i], fg, bg, line:true);
            }

            int selection = MenuSelection(options, x, y, alignment, fg, bg, selectedFg, selectedBg);
            if (reset)
            {
                for(int i = 0; i < options.Length; i++)
                {
                    ConsoleWriter.ClearLine(y + i);
                }
                Console.CursorTop = initY;
                Console.CursorLeft = initX;
            }

            Console.CursorVisible = true;
            return selection;
        }
    }
}