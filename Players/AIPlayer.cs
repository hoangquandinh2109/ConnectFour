using System;
using System.Collections.Generic;
using System.Threading;
using ConnectFour.UI;
using ConnectFour.Game;  // needed for Board

namespace ConnectFour.Players
{
    public class AIPlayer : PlayerBase
    {
        private Board _board;
        private Random _rand = new Random();

        // Updated constructor: now takes Board reference
        public AIPlayer(string name, char symbol, Board board) : base(name, symbol)
        {
            _board = board;
        }

        public override int GetMove()
        {
            ConsoleUI.AddMessage("Computer playing...");
            Thread.Sleep(1000); // fake brainstorm 1sec
            char opponentSymbol = (Symbol == 'X') ? 'O' : 'X';

            // 1) If AI can win next move, play it
            for (int c = 0; c < 7; c++)
            {
                if (!_board.IsColumnFull(c) && TryDropAndCheckWin(c, Symbol))
                    return c;
            }
            // 2) Block opponent win
            for (int c = 0; c < 7; c++)
            {
                if (!_board.IsColumnFull(c) && TryDropAndCheckWin(c, opponentSymbol))
                    return c;
            }
            // 3) Center
            if (!_board.IsColumnFull(3))
                return 3;
            // 4) Random valid
            List<int> valid = new List<int>();
            for (int c = 0; c < 7; c++)
                if (!_board.IsColumnFull(c))
                    valid.Add(c);
            if (valid.Count > 0)
                return valid[_rand.Next(valid.Count)];
            return 0; // fallback
        }

        private bool TryDropAndCheckWin(int column, char sym)
        {
            // Clone current grid
            char[,] snapshot = _board.CloneGrid();
            bool dropped = _board.DropDisc(column, sym);
            bool win = false;
            if (dropped)
            {
                win = _board.CheckWin(sym);
            }
            // Restore original grid
            _board.RestoreGrid(snapshot);
            return win;
        }
    }
}
