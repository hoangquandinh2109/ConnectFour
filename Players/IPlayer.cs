namespace ConnectFour.Players
{
    public interface IPlayer
    {
        string Name { get; }
        char Symbol { get; }
        int GetMove();
    }
}
