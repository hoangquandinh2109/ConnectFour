using System;
using ConnectFour.Utils;

namespace ConnectFour.UI
{
    public static class ConsoleUI
    {
        private static readonly string _title = "Connect Four";
        private static string _board = "";
        private static string _messages = "";

        public static void RenderScreen()
        {
            Console.Clear();
            Console.WriteLine(_title);
            Console.WriteLine();
            Console.WriteLine(_board);
            Console.Write(_messages);
        }

        public static void WriteBoard(string board)
        {
            _board = board;
            RenderScreen();
        }

        public static void AddMessage(string message)
        {
            _messages += $"\n{message}";
            RenderScreen();
        }

        public static void ClearMessages()
        {
            _messages = "";
            RenderScreen();
        }
        public static int ReadColumnInput(string prompt)
        {
            int column;
            while (true)
            {
                AddMessage(prompt);
                string? input = Console.ReadLine();

                if (input != null && Validator.IsValidColumn(input, out column))
                {
                    // Convert 1-based to 0-based index
                    return column - 1;
                }
                else
                {
                    ClearMessages();
                    AddMessage("Invalid input. Please enter a number between 1 and 7.");
                }
            }
        }

        public static bool AskPlayAgain()
        {
            AddMessage("Do you want to play again? (y/n): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "y" || input == "yes";
        }

        public static bool AskPlayWithAI()
        {
            AddMessage("Play against AI? (y/n): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "y" || input == "yes";
        }

        public static bool AskIfPlayer1PlayFirst()
        {
            AddMessage("Who play first? (1/2): ");
            string? input = Console.ReadLine()?.ToLower();
            return input == "1";
        }
    }
}
