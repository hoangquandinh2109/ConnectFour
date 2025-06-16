namespace ConnectFour.Utils
{
    public static class Validator
    {
        public static bool IsValidColumn(string input, out int column)
        {
            if (int.TryParse(input, out column) && column >= 1 && column <= 7)
                return true;

            return false;
        }
    }
}
