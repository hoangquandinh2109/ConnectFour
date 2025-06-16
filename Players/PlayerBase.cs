namespace ConnectFour.Players
{
    public abstract class PlayerBase : IPlayer
    {
        public string Name { get; protected set; }
        public char Symbol { get; protected set; }

        public PlayerBase(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public abstract int GetMove();
    }
}
