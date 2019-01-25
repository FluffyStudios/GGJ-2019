using System.Reflection;

namespace FluffyBox
{
    [UnityEngine.CreateAssetMenu(fileName = "GuiModifier", menuName = "GuiModifier", order = 1)]
    public class GuiModifier : UnityEngine.ScriptableObject
    {
        public string TargetProperty;
        
        public float StartValue = 0f;
        public float EndValue = 1f;        
        public float Duration = 1f;        
        public float StartDelay = 0f;

        public UnityEngine.AnimationCurve AnimationCurve = UnityEngine.AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public bool Loop = false;
        
        public PropertyInfo PropertyInfo
        {
            get;
            set;
        }

        public void Init()
        {
            this.PropertyInfo = typeof(GuiObject).GetProperty(this.TargetProperty);
        }
    }
}