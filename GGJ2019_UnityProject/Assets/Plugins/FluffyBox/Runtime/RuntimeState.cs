using System.Collections;

namespace FluffyBox
{
    public abstract class RuntimeState
    {
        public virtual void Begin()
        {
            UnityEngine.Debug.Log("Begin Runtime State: " + this.GetType().ToString());
        }

        public virtual void End()
        {
            UnityEngine.Debug.Log("End Runtime State: " + this.GetType().ToString());
        }
    }
}
