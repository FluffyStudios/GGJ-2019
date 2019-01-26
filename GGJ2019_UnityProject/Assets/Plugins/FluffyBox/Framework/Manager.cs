using System.Collections;

namespace FluffyBox
{
    public abstract class Manager : UnityEngine.MonoBehaviour
    {
        public bool Alive
        {
            get;
            protected set;
        }

        public abstract void RegisterService();

        public virtual IEnumerator Ignite()
        {
            this.Alive = true;
            this.RegisterService();

            yield break;
        }

        public abstract void OnIgnitionCompleted();        
    }
}