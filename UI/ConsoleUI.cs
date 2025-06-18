using System;
using ConnectFour.Game;
using ConnectFour.Utils;

namespace ConnectFour.UI
{
    public class ConsoleUI
    {
        private readonly Board _board;

        public ConsoleUI(Board board)
        {
            _board = board;
        }

        /// <summary>
        /// Prompt the user to choose a column [1-7].
        /// Does not clear the board here; just shows prompt and handles invalid input inline.
        /// </summary>
        public int ReadColumnInput(string prompt)
        {
            int column;
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (input != null && Validator.IsValidColumn(input, out column))
                {
                    // Convert 1-based to 0-based index
                    return column - 1;
                }
                else
                {
                    // Invalid input: show message but do not clear the board.
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    // Next iteration will reprint the prompt.
                }
            }
        }

        public bool AskPlayAgain()
        {
            Console.Write("Do you want to play again? (y/n): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "y" || input == "yes";
        }

        public bool AskPlayWithAI()
        {
            Console.Write("Play against AI? (y/n): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "y" || input == "yes";
        }

        public bool askIfPlayer1PlayFirst()
        {
            Console.Write("Who play first? (1/2): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "1";
        }

        /// <summary>
        /// If you have code elsewhere calling this, it will clear & redraw the board.
        /// But for ReadColumnInput, we removed ClearMessages to avoid clearing mid-input.
        /// You may keep or remove this method if unused.
        /// </summary>
        public void ClearMessages()
        {
            Console.Clear();
            _board.Display();
        }
    }
}
