using System;

namespace FluffyBox
{
    [Serializable]
    public class KeyAction
    {
        public string ActionName;
        public string PrimaryKeyString;
        public string SecondaryKeyString;
        public string AxisName;

        public UnityEngine.KeyCode PrimaryKeyCode
        {
            get;
            private set;
        }

        public UnityEngine.KeyCode SecondaryKeyCode
        {
            get;
            private set;
        }

        public void Initialize()
        {
            this.PrimaryKeyCode = !string.IsNullOrEmpty(this.PrimaryKeyString) ? (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), this.PrimaryKeyString) : UnityEngine.KeyCode.None;
            this.SecondaryKeyCode = !string.IsNullOrEmpty(this.SecondaryKeyString) ? (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), this.SecondaryKeyString) : UnityEngine.KeyCode.None;
        }

        public static KeyAction CreateFromJSON(string path)
        {
            KeyAction keyAction = UnityEngine.JsonUtility.FromJson<KeyAction>(path);
            keyAction.Initialize();
            
            return keyAction;
        }
    }
}