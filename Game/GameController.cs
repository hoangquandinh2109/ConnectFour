using ConnectFour.Players;
using ConnectFour.UI;

namespace ConnectFour.Game
{
    public class GameController
    {
        private Board board;
        private IPlayer player1;
        private IPlayer player2;
        private IPlayer currentPlayer;
        private ConsoleUI ui;

        public GameController()
        {
            board = new Board();
            ui = new ConsoleUI(board);
            player1 = new HumanPlayer("Player 1", 'X', ui);

            if (ui.AskPlayWithAI())
            {
                player2 = new AIPlayer("Computer", 'O');
            }
            else
            {
                player2 = new HumanPlayer("Player 2", 'O', ui);
            }

            currentPlayer = ui.askIfPlayer1PlayFirst() ? player1 : player2;
            Console.Clear();
        }

        public void Run()
        {
            bool playAgain;
            do
            {
                currentPlayer = player1;
                board.Display();

                while (true)
                {
                    int columnIndex = currentPlayer.GetMove();
                    ui.ClearMessages();
                    bool success = board.DropDisc(columnIndex, currentPlayer.Symbol);

                    if (!success)
                    {
                        Console.WriteLine("Column is full. Try again.");
                        continue;
                    }

                    if (board.CheckWin(currentPlayer.Symbol))
                    {
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        break;
                    }

                    if (board.IsFull())
                    {
                        Console.WriteLine("The game is a tie!");
                        break;
                    }

                    SwitchPlayer();
                }

                playAgain = ui.AskPlayAgain();
                if (playAgain)
                {
                    board.Reset();
                }
            }
            while (playAgain);
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == player1 ? player2 : player1;
        }
    }
}
