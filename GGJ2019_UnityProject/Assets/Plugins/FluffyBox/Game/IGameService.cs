using System.Collections;

namespace FluffyBox
{
    public interface IGameService : IService
    {
        event System.EventHandler<GameStateChangedEventArgs> GameStateChanged;
        
        GameState CurrentGameState
        {
            get;
        }

        void ChangeGameState(GameState newState);
    }
}
