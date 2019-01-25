namespace FluffyBox
{
    public abstract class GameState
    {
        public virtual void Begin()
        {
            UnityEngine.Debug.Log("Begin Game State: " + this.GetType().ToString());
        }

        public virtual void End()
        {
            UnityEngine.Debug.Log("End Game State: " + this.GetType().ToString());
        }
    }
}
