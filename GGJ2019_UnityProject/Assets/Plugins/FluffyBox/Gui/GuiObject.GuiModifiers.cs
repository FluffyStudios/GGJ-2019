using System.Collections.Generic;

namespace FluffyBox
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.CanvasGroup))]
    [UnityEngine.RequireComponent(typeof(UnityEngine.CanvasRenderer))]
    public partial class GuiObject : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField]
        public List<GuiModifier> GuiModifiers = new List<GuiModifier>();

        private Dictionary<GuiModifier, UnityEngine.Coroutine> animationCoroutinesByGuiModifier = new Dictionary<GuiModifier, UnityEngine.Coroutine>();
        private float currentAnimationTime;

        public bool ModifiersRunning
        {
            get
            {
                return this.animationCoroutinesByGuiModifier.Count > 0;
            }
        }

        public void StartModifiers(bool forward = true)
        {
            for (int i = 0; i < this.GuiModifiers.Count; i++)
            {
                this.StartModifier(this.GuiModifiers[i], forward);
            }
        }

        public void StopModifiers(bool reset = false, bool resetToEnd = false)
        {
            for (int i = 0; i < this.GuiModifiers.Count; i++)
            {
                this.StopModifier(this.GuiModifiers[i], reset, resetToEnd);
            }
        }

        public void StartModifier(int index, bool forward = true)
        {
            if (index < this.GuiModifiers.Count)
            {
                this.StartModifier(this.GuiModifiers[index], forward);
            }
        }

        public void StartModifier(GuiModifier guiModifier, bool forward = true)
        {
            if (guiModifier == null)
            {
                UnityEngine.Debug.LogError("Trying to start a null modifier.");
                return;
            }

            if (guiModifier.PropertyInfo == null)
            {
                UnityEngine.Debug.LogError("Trying to start a modifier with no Property Info. Check Target.");
                return;
            }

            if (guiModifier.Duration <= 0)
            {
                this.StopModifier(guiModifier, true, forward);
                return;
            }

            this.animationCoroutinesByGuiModifier.Add(guiModifier, this.StartCoroutine(this.Coroutine_AnimateModifier(guiModifier, forward)));
        }

        public void StopModifier(int index, bool reset = true, bool resetToEnd = false)
        {
            if (index < this.GuiModifiers.Count)
            {
                this.StopModifier(this.GuiModifiers[index], reset, resetToEnd);
            }
            else
            {
                UnityEngine.Debug.LogError("There is no GuiModifier at index " + index);
            }
        }

        public void StopModifier(GuiModifier guiModifier, bool reset = false, bool resetToEnd = false)
        {
            if (guiModifier == null)
            {
                UnityEngine.Debug.LogError("Trying to stop a null modifier.");
                return;
            }
            
            if (this.animationCoroutinesByGuiModifier.ContainsKey(guiModifier))
            {
                this.StopCoroutine(this.animationCoroutinesByGuiModifier[guiModifier]);
                this.animationCoroutinesByGuiModifier.Remove(guiModifier);
            }

            if (reset)
            {
                guiModifier.PropertyInfo.SetValue(this, resetToEnd ? guiModifier.EndValue : guiModifier.StartValue, null);
            }
        }

        private System.Collections.IEnumerator Coroutine_AnimateModifier(GuiModifier guiModifier, bool forward)
        {
            this.currentAnimationTime = 0;
            float startTime = UnityEngine.Time.time;
            while (this.currentAnimationTime < guiModifier.Duration)
            {
                this.currentAnimationTime = UnityEngine.Time.time - startTime;
                float timeProgress = this.currentAnimationTime / guiModifier.Duration;
                float valueProgress = guiModifier.AnimationCurve.Evaluate(timeProgress);
                float startValue = forward ? guiModifier.StartValue : guiModifier.EndValue;
                float endValue = forward ? guiModifier.EndValue : guiModifier.StartValue;
                float currentValue = UnityEngine.Mathf.Lerp(startValue, endValue, valueProgress);

                guiModifier.PropertyInfo.SetValue(this, currentValue, null);

                yield return null;
            }

            this.StopModifier(guiModifier);
        }

        private void InitModifiers()
        {
            for (int i = 0; i < this.GuiModifiers.Count; i++)
            {
                this.GuiModifiers[i].Init();
            }
        }
    }
}
