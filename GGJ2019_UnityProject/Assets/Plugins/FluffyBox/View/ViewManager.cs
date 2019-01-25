using System;
using System.Collections;

namespace FluffyBox
{
    public partial class ViewManager : Manager, IViewService
    {
        public event EventHandlerOnLoadView OnLoadView;

        public View MainView
        {
            get;
            set;
        }

        public override void RegisterService()
        {
            Services.AddService<IViewService>(this);
        }

        public override IEnumerator Ignite()
        {
            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
        }

        public virtual IEnumerator LoadMainView(string sceneName)
        {
            // Unload current main view if existing.
            if (this.MainView != null)
            {
                this.MainView.OnUnload();
            }

            // Load new main view. TODO : do something cleanier than a GameObject.Find...
            UnityEngine.AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            yield return async;
            this.MainView = UnityEngine.GameObject.Find(sceneName).transform.GetComponent<View>();

            if (this.MainView == null)
            {
                UnityEngine.Debug.LogError("The scene " + sceneName + " has encountered an error while loading. Check the name of the main container.");
                yield break;
            }

            this.MainView.transform.parent = this.transform;

            yield return this.MainView.StartCoroutine(this.MainView.OnLoad());

            if (this.OnLoadView != null)
            {
                this.OnLoadView(this, new EventArgsOnLoadView(this.MainView));
            }

            yield break;
        }
    }
}
