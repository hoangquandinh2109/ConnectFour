using ConnectFour.UI;

namespace ConnectFour.Players
{
    public class HumanPlayer : PlayerBase
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol)
        {}

        public override int GetMove()
        {
            return ConsoleUI.ReadColumnInput($"{Name}'s turn (Symbol: {Symbol}). Choose column [1-7]: ");
        }
    }
}
