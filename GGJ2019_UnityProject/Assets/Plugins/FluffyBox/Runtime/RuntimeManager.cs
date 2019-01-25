using System.Collections;

namespace FluffyBox
{
    public partial class RuntimeManager : Manager, IRuntimeService
    {
        public event System.EventHandler<RuntimeStateChangedEventArgs> RuntimeStateChanged;

        public RuntimeState CurrentRuntimeState
        {
            get;
            private set;
        }

        public override void RegisterService()
        {
            Services.AddService<IRuntimeService>(this);
        }
        
        public override void OnIgnitionCompleted()
        {
        }

        public void ChangeRuntimeState(RuntimeState newState)
        {
            // End current (old) state.
            RuntimeState oldState = this.CurrentRuntimeState;
            if (oldState != null)
            {
                oldState.End();
            }

            // Change current state and begin the new one.
            this.CurrentRuntimeState = newState;
            if (this.CurrentRuntimeState != null)
            {
                this.CurrentRuntimeState.Begin();
            }

            // Invoke Event.
            if (this.RuntimeStateChanged != null)
            {
                this.RuntimeStateChanged.Invoke(this, new RuntimeStateChangedEventArgs(oldState, newState));
            }
        }
    }
}
