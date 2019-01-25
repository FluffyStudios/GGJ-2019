using System.Collections;
using UnityEngine;

namespace FluffyBox
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GuiObject))]
    public class GuiPanel : MonoBehaviour
    {
        private CanvasGroup privateCanvasGroup;
        private GuiObject privateGuiObject;

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (this.privateCanvasGroup == null)
                {
                    this.privateCanvasGroup = this.GetComponent<CanvasGroup>();
                }

                return this.privateCanvasGroup;
            }
        }

        public GuiObject GuiObject
        {
            get
            {
                if (this.privateGuiObject == null)
                {
                    this.privateGuiObject = this.GetComponent<GuiObject>();
                }

                return this.privateGuiObject;
            }
        }

        public virtual void Load()
        {
            this.StartCoroutine(this.OnLoad());
        }

        public virtual void Unload()
        {
            this.StartCoroutine(this.OnUnload());
        }

        public virtual IEnumerator OnLoad()
        {
            yield break;
        }

        public virtual IEnumerator OnUnload()
        {
            yield break;
        }

        public virtual void Show(bool animated = true)
        {
            this.StartCoroutine(this.OnBeginShow(animated));
        }

        public virtual void Hide(bool animated = true)
        {
            this.StartCoroutine(this.OnBeginHide(animated));
        }
        
        protected virtual IEnumerator OnBeginShow(bool animated = true)
        {
            if (animated)
            {
                this.GuiObject.StartModifiers();
                while (this.GuiObject.ModifiersRunning)
                {
                    yield return null;
                }
            }

            this.OnEndShow();
            yield break;
        }

        protected virtual void OnEndShow()
        {
            this.GuiObject.Alpha = 1f;
            this.CanvasGroup.interactable = true;
            this.CanvasGroup.blocksRaycasts = true;
        }

        protected virtual IEnumerator OnBeginHide(bool animated = true)
        {
            if (animated)
            {
                this.GuiObject.StartModifiers(false);
                while (this.GuiObject.ModifiersRunning)
                {
                    yield return null;
                }
            }

            this.OnEndHide();
            yield break;
        }

        protected virtual void OnEndHide()
        {
            this.GuiObject.Alpha = 0f;
            this.CanvasGroup.interactable = false;
            this.CanvasGroup.blocksRaycasts = false;
        }
    }
}
