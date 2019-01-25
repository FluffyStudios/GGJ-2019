public partial class Game
{
    private static IGameService privateGameService;
    private static Game privateGame;

    public static IGameService GameService
    {
        get
        {
            if (Game.privateGameService == null)
            {
                Game.privateGameService = FluffyBox.Services.GetService<IGameService>();
            }

            return Game.privateGameService;
        }
    }

    public static Game ActiveGame
    {
        get
        {
            return Game.GameService.Game;
        }
    }
}