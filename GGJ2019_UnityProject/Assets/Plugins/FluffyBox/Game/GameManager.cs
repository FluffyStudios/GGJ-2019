using System.Collections;

namespace FluffyBox
{
    public partial class GameManager : Manager, IGameService
    {
        public event System.EventHandler<GameStateChangedEventArgs> GameStateChanged;

        public GameState CurrentGameState
        {
            get;
            private set;
        }

        public override void RegisterService()
        {
            Services.AddService<IGameService>(this);
        }

        public override IEnumerator Ignite()
        {
            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
        }

        public void ChangeGameState(GameState newState)
        {
            // End current (old) state.
            GameState oldState = this.CurrentGameState;
            if (oldState != null)
            {
                oldState.End();
            }

            // Change current state and begin the new one.
            this.CurrentGameState = newState;
            if (this.CurrentGameState != null)
            {
                this.CurrentGameState.Begin();
            }

            // Invoke Event.
            if (this.GameStateChanged != null)
            {
                this.GameStateChanged.Invoke(this, new GameStateChangedEventArgs(oldState, newState));
            }
        }
    }
}
