using System.Collections;

namespace FluffyBox
{
    public interface IRuntimeService : IService
    {        
        event System.EventHandler<RuntimeStateChangedEventArgs> RuntimeStateChanged;

        RuntimeState CurrentRuntimeState
        {
            get;
        }

        void ChangeRuntimeState(RuntimeState newState);
    }
}
