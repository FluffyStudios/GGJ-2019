using System.Collections;

namespace FluffyBox
{
    public class RuntimeStateChangedEventArgs : System.EventArgs
    {
        public RuntimeState OldState
        {
            get;
            private set;
        }

        public RuntimeState NewState
        {
            get;
            private set;
        }

        public RuntimeStateChangedEventArgs(RuntimeState oldState, RuntimeState newState)
        {
            this.OldState = oldState;
            this.NewState = newState;
        }
    }
}
