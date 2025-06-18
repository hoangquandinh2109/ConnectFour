using System;
using ConnectFour.Players;
using ConnectFour.UI;
using ConnectFour.Game;  // for Board

namespace ConnectFour.Game
{
    public class GameController
    {
        private Board board;
        private IPlayer player1;
        private IPlayer player2;
        private IPlayer currentPlayer;  // non-nullable, initialized in constructor
        private ConsoleUI ui;
        private bool vsAI;
        private Random rand = new Random();

        public GameController()
        {
            // Initialize board and UI
            board = new Board();
            ui = new ConsoleUI(board);

            // Player1 is always human with symbol 'X'
            player1 = new HumanPlayer("Player 1", 'X', ui);

            // Ask if user wants to play against AI
            vsAI = ui.AskPlayWithAI();
            if (vsAI)
            {
                // Pass board reference to AIPlayer for lookahead logic
                player2 = new AIPlayer("Computer", 'O', board);
            }
            else
            {
                // Two-player: second human with symbol 'O'
                player2 = new HumanPlayer("Player 2", 'O', ui);
            }

            // Initialize currentPlayer to a non-null default (satisfies non-nullable field)
            currentPlayer = player1;

            // Clear console before starting
            Console.Clear();
        }

        public void Run()
        {
            bool playAgain;
            do
            {
                // Determine who starts this round
                if (vsAI)
                {
                    bool humanStarts = rand.Next(2) == 0;
                    currentPlayer = humanStarts ? player1 : player2;
                    Console.Clear();
                    Console.WriteLine($"{(currentPlayer == player1 ? "Player 1" : "Computer")} starts this round.");
                }
                else
                {
                    currentPlayer = player1;
                    Console.Clear();
                    Console.WriteLine("Player 1 starts this round.");
                }

                // Reset and display initial board
                board.Reset();
                Console.Clear();
                board.Display();

                // Main turn loop
                while (true)
                {
                    // Prompt & get move.
                    // For human: HumanPlayer.GetMove will show:
                    //   "Player X's turn (Symbol: Y). Choose column [1-7]: "
                    int columnIndex = currentPlayer.GetMove();

                    // Attempt to drop disc
                    bool success = board.DropDisc(columnIndex, currentPlayer.Symbol);
                    if (!success)
                    {
                        // Invalid or full column: clear & redraw then show error and re-prompt
                        Console.Clear();
                        board.Display();
                        Console.WriteLine("Column is full or invalid. Try again.");
                        continue;  // same player retries
                    }

                    // Successful drop: clear & redraw board
                    Console.Clear();
                    board.Display();

                    // Check for win
                    if (board.CheckWin(currentPlayer.Symbol))
                    {
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        break;
                    }

                    // Check for tie
                    if (board.IsFull())
                    {
                        Console.WriteLine("The game is a tie!");
                        break;
                    }

                    // Switch to next player
                    SwitchPlayer();

                    // Optionally show whose turn next before prompt.
                    // But since GetMove() prompt includes turn info, you may omit this line to avoid duplication.
                    // If you wish, uncomment:
                    // Console.WriteLine($"{currentPlayer.Name}'s turn (Symbol: {currentPlayer.Symbol})");
                }

                // After game over, ask replay
                playAgain = ui.AskPlayAgain();
                // Loop repeats if true: chooses new starter, resets board, etc.
            }
            while (playAgain);
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }
    }
}


