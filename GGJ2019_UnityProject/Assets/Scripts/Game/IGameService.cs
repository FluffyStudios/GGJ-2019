public interface IGameService : FluffyBox.IGameService
{
    Game Game
    {
        get;
    }

    void CreateGame();
}
