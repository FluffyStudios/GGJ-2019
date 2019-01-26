using System.Collections;

namespace FluffyBox
{
    public partial class GuiManager : Manager, IGuiService
    {
        public UnityEngine.Canvas MainCanvas;

        public GuiWindow[] GuiWindows
        {
            get;
            private set;
        }

        public override void RegisterService()
        {
            Services.AddService<IGuiService>(this);
        }

        public override IEnumerator Ignite()
        {
            this.MainCanvas.enabled = false;

            yield return this.StartCoroutine(this.OnLoad());

            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
            this.MainCanvas.enabled = true;

            this.StartCoroutine(this.UpdateWindowsInteractionCoroutine());
        }

        public virtual IEnumerator OnLoad()
        {
            this.GuiWindows = this.GetComponentsInChildren<GuiWindow>();

            for (int i = 0, lth = this.GuiWindows.Length; i < lth; ++i)
            {
                yield return this.GuiWindows[i].StartCoroutine(this.GuiWindows[i].OnLoad());
            }

            for (int i = 0, lth = this.GuiWindows.Length; i < lth; ++i)
            {
                this.GuiWindows[i].Hide(false);
            }
        }

        public void ShowWindow<T>(bool animated = true)
        {
            for (int i = 0; i < this.GuiWindows.Length; i++)
            {
                if (this.GuiWindows[i].GetType() == typeof(T))
                {
                    this.GuiWindows[i].Show(animated);
                    return;
                }
            }
        }

        public void ShowWindow(GuiWindow guiWindow, bool animated = true)
        {
            if (guiWindow != null)
            {
                guiWindow.Show(animated);
            }
        }

        public void HideWindow<T>(bool animated = true)
        {
            for (int i = 0; i < this.GuiWindows.Length; i++)
            {
                if (this.GuiWindows[i].GetType() == typeof(T))
                {
                    this.GuiWindows[i].Hide(animated);
                    return;
                }
            }
        }

        public void HideWindow(GuiWindow guiWindow, bool animated = true)
        {
            if (guiWindow != null)
            {
                guiWindow.Hide(animated);
            }
        }

        public T GetWindow<T>() where T : GuiWindow
        {
            for (int i = 0; i < this.GuiWindows.Length; i++)
            {
                if (this.GuiWindows[i].GetType() == typeof(T))
                {
                    return this.GuiWindows[i] as T;
                }
            }

            return null;
        }
        
        private IEnumerator UpdateWindowsInteractionCoroutine()
        {
            while (this.Alive)
            {
                for (int i = 0; i < this.GuiWindows.Length; i++)
                {
                    this.GuiWindows[i].UpdateInteraction();
                }

                yield return null;
            }
        }
    }
}
