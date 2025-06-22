using System;
using ConnectFour.UI;

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

        public void Initialize()
        {
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    _grid[r, c] = _emptyCell;
        }

        public void Reset()
        {
            Initialize();
            Display();
        }

        public bool DropDisc(int columnIndex, char symbol)
        {
            for (int rowIndex = _rows - 1; rowIndex >= 0; rowIndex--)
            {
                if (_grid[rowIndex, columnIndex] == _emptyCell)
                {
                    _grid[rowIndex, columnIndex] = symbol;
                    Display();
                    return true; // drop done
                }
            }

            return false; // column full
        }

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

        public bool IsFull()
        {
            for (int c = 0; c < _columns; c++)
            {
                if (_grid[0, c] == _emptyCell)
                    return false;
            }
            return true;
        }

        public bool IsColumnFull(int col)
        {
            if (col < 0 || col >= _columns)
                return true;
            return _grid[0, col] != _emptyCell;
        }

        public void Display()
        {
            string boardContent = " 1 2 3 4 5 6 7\n";
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    boardContent += $" {_grid[r, c]}";
                }
                if (r < _rows - 1)
                    boardContent += "\n";
            }
            ConsoleUI.WriteBoard(boardContent);
        }

        public char[,] CloneGrid()
        {
            var copy = new char[_rows, _columns];
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    copy[r, c] = _grid[r, c];
            return copy;
        }

        public void RestoreGrid(char[,] snapshot)
        {
            if (snapshot == null) return;
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _columns; c++)
                    _grid[r, c] = snapshot[r, c];
        }
    }
}
