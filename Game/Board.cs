namespace ConnectFour.Game
{
    public class Board
    {
        private const int Rows = 6;
        private const int Columns = 7;
        private readonly char[,] grid;

        public Board()
        {
            grid = new char[Rows, Columns];
        }

        public bool DropDisc(int columnIndex, char symbol)
        {
            Console.WriteLine($"drop {symbol} into column {columnIndex + 1}");
            // TODO: Drop disc into specified column
            // return false if column is full and cannot be drop more

            return true; // true is no error
        }

        public bool CheckWin(char symbol)
        {
            // TODO Check for 4 in a row
            return false;
        }

        public bool IsFull()
        {
            // TODO Check if board is completely filled
            return false;
        }

        public void Display()
        {
            // TODO Display the board in the console
            Console.WriteLine("Board here...\n");
        }

        public void Reset()
        {
            // TODO Clear the board
        }
    }
}
