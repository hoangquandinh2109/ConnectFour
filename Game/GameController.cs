using System;
using ConnectFour.Players;
using ConnectFour.UI;

namespace ConnectFour.Game
{
    public class GameController
    {
        private Board board;
        private IPlayer player1;
        private IPlayer player2;
        private IPlayer currentPlayer;  // non-nullable, initialized in constructor

        public GameController()
        {
            board = new Board();
            player1 = new HumanPlayer("Player 1", 'X');
            player2 = new HumanPlayer("Player 2", 'O');
            currentPlayer = player1;
        }

        public void Run()
        {
            bool playAgain;
            do
            {
                board.Display();
                bool vsAI = ConsoleUI.AskPlayWithAI();
                ConsoleUI.ClearMessages();

                // Determine who starts this round
                if (vsAI)
                {
                    player2 = new AIPlayer("Computer", 'O', board);
                    bool isHumanFirst = new Random().Next(2) == 0;
                    currentPlayer = isHumanFirst ? player1 : player2;
                }
                else
                {
                    player2 = new HumanPlayer("Player 2", 'O');
                    currentPlayer = ConsoleUI.AskIfPlayer1PlayFirst() ? player1 : player2;
                    ConsoleUI.ClearMessages();
                }

                ConsoleUI.AddMessage($"{currentPlayer.Name} starts this match.\n");

                bool isGameOver = false;

                while (!isGameOver)
                {
                    int columnIndex = currentPlayer.GetMove();
                    bool success = board.DropDisc(columnIndex, currentPlayer.Symbol);

                    if (!success)
                    {
                        ConsoleUI.ClearMessages();
                        ConsoleUI.AddMessage("Column is full. Try again.");
                        continue;  // same player retries
                    }

                    ConsoleUI.ClearMessages();

                    if (board.CheckWin(currentPlayer.Symbol))
                    {
                        ConsoleUI.AddMessage($"{currentPlayer.Name} wins!");
                        isGameOver = true;
                    }

                    if (board.IsFull())
                    {
                        ConsoleUI.AddMessage("The game is a tie!");
                        isGameOver = true;
                    }

                    SwitchPlayer();
                }

                playAgain = ConsoleUI.AskPlayAgain();
                ConsoleUI.ClearMessages();

                if (playAgain)
                {
                    board.Reset();
                }
            }
            while (playAgain);
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }
    }
}
