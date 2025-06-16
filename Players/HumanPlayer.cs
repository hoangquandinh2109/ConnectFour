using ConnectFour.UI;

namespace ConnectFour.Players
{
    public class HumanPlayer : PlayerBase
    {
        private readonly ConsoleUI ui;

        public HumanPlayer(string name, char symbol, ConsoleUI ui) : base(name, symbol)
        {
            this.ui = ui;
        }

        public override int GetMove()
        {
            return ui.ReadColumnInput($"{Name}'s turn (Symbol: {Symbol}). Choose column [1-7]: ");
        }
    }
}
