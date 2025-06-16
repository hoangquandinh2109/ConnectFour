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

        public int ReadColumnInput(string prompt)
        {
            int column;
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (input != null && Validator.IsValidColumn(input, out column))
                {
                    return column - 1;
                }
                else
                {
                    ClearMessages();
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
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

        public void ClearMessages()
        {
            Console.Clear();
            _board.Display();
        }
  }
}
