using System.Collections.Generic;

namespace FluffyBox
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.CanvasGroup))]
    [UnityEngine.RequireComponent(typeof(UnityEngine.CanvasRenderer))]
    public partial class GuiObject : UnityEngine.MonoBehaviour
    {        
        private UnityEngine.CanvasGroup privateCanvasGroup;
        private UnityEngine.RectTransform privateRectTransform;
        
        public UnityEngine.CanvasGroup CanvasGroup
        {
            get
            {
                if (this.privateCanvasGroup == null)
                {
                    this.privateCanvasGroup = this.GetComponent<UnityEngine.CanvasGroup>();
                }

                return this.privateCanvasGroup;
            }
        }

        public UnityEngine.RectTransform RectTransform
        {
            get
            {
                if (this.privateRectTransform == null)
                {
                    this.privateRectTransform = this.GetComponent<UnityEngine.RectTransform>();
                }

                return this.privateRectTransform;
            }
        }

        public float Alpha
        {
            get
            {
                return this.CanvasGroup.alpha;
            }

            set
            {
                this.CanvasGroup.alpha = UnityEngine.Mathf.Clamp01(value);
            }
        } 

        public float Left
        {
            get
            {
                return this.RectTransform.offsetMin.x;
            }

            set
            {
                this.RectTransform.offsetMin = new UnityEngine.Vector2(value, this.RectTransform.offsetMin.y);
            }
        }

        public float Right
        {
            get
            {
                return this.RectTransform.offsetMax.x;
            }

            set
            {
                this.RectTransform.offsetMax = new UnityEngine.Vector2(value, this.RectTransform.offsetMax.y);
            }
        }

        public float Top
        {
            get
            {
                return this.RectTransform.offsetMin.y;
            }

            set
            {
                this.RectTransform.offsetMin = new UnityEngine.Vector2(this.RectTransform.offsetMin.x, value);
            }
        }

        public float Bottom
        {
            get
            {
                return this.RectTransform.offsetMax.y;
            }

            set
            {
                this.RectTransform.offsetMax = new UnityEngine.Vector2(this.RectTransform.offsetMax.x, value);
            }
        }

        public bool Visible
        {
            get
            {
                return this.Alpha > 0;
            }

            set
            {
                // TODO : Find a way to set the alpha through this setter without interfering with the alpha value. 
            }
        }

        private void Awake()
        {
            this.InitModifiers();
        }
    }
}
