using System.Collections;

namespace FluffyBox
{
    public class View : UnityEngine.MonoBehaviour
    {
        protected bool isLoaded;

        public virtual IEnumerator OnLoad(bool debug = false)
        {
            this.isLoaded = true;

            yield break;
        }

        public virtual void OnUnload()
        {
            this.isLoaded = false;
            UnityEngine.GameObject.Destroy(this.gameObject);
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (!this.isLoaded && UnityEngine.GUI.Button(new UnityEngine.Rect(10, 10, 75, 25), "Load"))
            {
                this.StartCoroutine(this.OnLoad(true));
            }
        }
#endif
    }
}
