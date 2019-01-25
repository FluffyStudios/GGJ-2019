using System.Collections;

namespace FluffyBox
{
    public class GameStateChangedEventArgs : System.EventArgs
    {
        public GameState OldState
        {
            get;
            private set;
        }

        public GameState NewState
        {
            get;
            private set;
        }

        public GameStateChangedEventArgs(GameState oldState, GameState newState)
        {
            this.OldState = oldState;
            this.NewState = newState;
        }
    }
}
