using ConnectFour.Game;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController controller = new GameController();
            controller.Run();
        }
    }
}
