namespace ConnectFour.Players
{
    public class AIPlayer : PlayerBase
    {
        public AIPlayer(string name, char symbol) : base(name, symbol)
        { }

        public override int GetMove()
        {
            // TODO return column that AI chose

            return 0; // fallback (should never happen if checked properly)
        }
    }
}