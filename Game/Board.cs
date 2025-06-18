using System;

using System;

namespace ConnectFour.Game
{
    public class Board
    {
        private readonly char[,] _grid;
        private readonly int _rows = 6;
        private readonly int _columns = 7;
        private readonly char _emptyCell = '.';

        public Board()
        {
            _grid = new char[_rows, _columns];
            Initialize();
        }

        /// <summary>
        /// Fill the grid with empty cells.
        /// </summary>
        public void Initialize()
        {
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    _grid[r, c] = _emptyCell;
        }

        /// <summary>
        /// Reset the board to initial empty state.
        /// </summary>
        public void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Display the board to console: columns labeled 1-7, rows top to bottom.
        /// </summary>
        public void Display()
        {
            Console.WriteLine();
            Console.WriteLine(" 1 2 3 4 5 6 7");
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    Console.Write(" " + _grid[r, c]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Attempt to drop a disc of given symbol into given column (0-based).
        /// Returns true if placed; false if invalid column or column full.
        /// </summary>
        public bool DropDisc(int column, char symbol)
        {
            if (column < 0 || column >= _columns)
                return false;

            for (int row = _rows - 1; row >= 0; row--)
            {
                if (_grid[row, column] == _emptyCell)
                {
                    _grid[row, column] = symbol;
                    return true;
                }
            }

            return false; // column full
        }

        /// <summary>
        /// Check if board is full (tie).
        /// </summary>
        public bool IsFull()
        {
            for (int c = 0; c < _columns; c++)
            {
                if (_grid[0, c] == _emptyCell)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Check if the given symbol has a four-in-a-row anywhere.
        /// </summary>
        public bool CheckWin(char symbol)
        {
            // Horizontal
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c <= _columns - 4; c++)
                    if (_grid[r, c] == symbol &&
                        _grid[r, c + 1] == symbol &&
                        _grid[r, c + 2] == symbol &&
                        _grid[r, c + 3] == symbol)
                        return true;

            // Vertical
            for (int c = 0; c < _columns; c++)
                for (int r = 0; r <= _rows - 4; r++)
                    if (_grid[r, c] == symbol &&
                        _grid[r + 1, c] == symbol &&
                        _grid[r + 2, c] == symbol &&
                        _grid[r + 3, c] == symbol)
                        return true;

            // Diagonal \ 
            for (int r = 0; r <= _rows - 4; r++)
                for (int c = 0; c <= _columns - 4; c++)
                    if (_grid[r, c] == symbol &&
                        _grid[r + 1, c + 1] == symbol &&
                        _grid[r + 2, c + 2] == symbol &&
                        _grid[r + 3, c + 3] == symbol)
                        return true;

            // Diagonal /
            for (int r = 3; r < _rows; r++)
                for (int c = 0; c <= _columns - 4; c++)
                    if (_grid[r, c] == symbol &&
                        _grid[r - 1, c + 1] == symbol &&
                        _grid[r - 2, c + 2] == symbol &&
                        _grid[r - 3, c + 3] == symbol)
                        return true;

            return false;
        }

        /// <summary>
        /// Check if the top cell of given column is non-empty.
        /// Used to detect full column or for AI move validation.
        /// </summary>
        public bool IsColumnFull(int col)
        {
            if (col < 0 || col >= _columns)
                return true;
            return _grid[0, col] != _emptyCell;
        }

        /// <summary>
        /// Create a deep copy of the grid array.
        /// Used by AI for lookahead simulation.
        /// </summary>
        public char[,] CloneGrid()
        {
            var copy = new char[_rows, _columns];
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    copy[r, c] = _grid[r, c];
            return copy;
        }

        /// <summary>
        /// Restore the grid from a previously cloned snapshot.
        /// Used by AI after simulated moves.
        /// </summary>
        public void RestoreGrid(char[,] snapshot)
        {
            if (snapshot == null) return;
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    _grid[r, c] = snapshot[r, c];
        }
    }
}
