using System.Collections;
using FluffyBox.Extensions;

public class GameManager : FluffyBox.GameManager, IGameService
{
    public Game Game
    {
        get;
        private set;
    }

    public override void RegisterService()
    {
        FluffyBox.Services.AddService<IGameService>(this);
    }

    public void CreateGame()
    {
        this.Game = new Game();
        this.ChangeGameState(new GameState_Init());
    }
}