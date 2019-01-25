using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluffyBox
{
    public partial class KeyMappingManager : Manager, IKeyMappingService
    {
        public static string DefaultKeyMapName = "Default";

        protected List<KeyMap> KeyMaps = null;
        
        public KeyMap ActiveKeyMap
        {
            get;
            set;
        }

        protected KeyMap DefaultKeyMap
        {
            get;
            set;
        }

        public override void RegisterService()
        {
            Services.AddService<IKeyMappingService>(this);
        }

        public override IEnumerator Ignite()
        {
            this.GenerateKeyMaps();
            this.ActiveKeyMap = this.DefaultKeyMap; // temp

            yield return base.Ignite();
        }

        public override void OnIgnitionCompleted()
        {
        }

        public bool GetKeyAction(string actionName)
        {
            KeyAction keyAction = this.ActiveKeyMap.KeyActions.Find(match => match.ActionName == actionName);

            if (keyAction == null)
            {
                UnityEngine.Debug.LogError("The action named " + actionName + " doesn't exists !");
                return false;
            }

            return UnityEngine.Input.GetKey(keyAction.PrimaryKeyCode) || UnityEngine.Input.GetKey(keyAction.SecondaryKeyCode);
        }

        public bool GetKeyActionDown(string actionName)
        {
            KeyAction keyAction = this.ActiveKeyMap.KeyActions.Find(match => match.ActionName == actionName);

            if (keyAction == null)
            {
                UnityEngine.Debug.LogError("The action named " + actionName + " doesn't exists !");
                return false;
            }

            return UnityEngine.Input.GetKeyDown(keyAction.PrimaryKeyCode) || UnityEngine.Input.GetKeyDown(keyAction.SecondaryKeyCode);
        }

        public bool GetKeyActionUp(string actionName)
        {
            KeyAction keyAction = this.ActiveKeyMap.KeyActions.Find(match => match.ActionName == actionName);

            if (keyAction == null)
            {
                UnityEngine.Debug.LogError("The action named " + actionName + " doesn't exists !");
                return false;
            }

            return UnityEngine.Input.GetKeyUp(keyAction.PrimaryKeyCode) || UnityEngine.Input.GetKeyUp(keyAction.SecondaryKeyCode);
        }

        public float GetKeyActionAxis(string actionName, bool inverted = false)
        {
            KeyAction keyAction = this.ActiveKeyMap.KeyActions.Find(match => match.ActionName == actionName);

            if (keyAction == null)
            {
                UnityEngine.Debug.LogError("The action named " + actionName + " doesn't exists !");
                return 0;
            }

            float axisValue = UnityEngine.Input.GetAxis(keyAction.AxisName);
            return inverted ? -axisValue : axisValue;
        }

        protected virtual void GenerateKeyMaps()
        {
            this.KeyMaps = new List<KeyMap>();
            
            // Get KeyMaps from external folder.
            string path = Application.KeyMapsPath;
            if (Directory.Exists(path))
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    KeyMap newKeyMap = new KeyMap(directory);
                    this.KeyMaps.Add(newKeyMap);
                    if (this.DefaultKeyMap == null && newKeyMap.MapName == KeyMappingManager.DefaultKeyMapName)
                    {
                        this.DefaultKeyMap = newKeyMap;
                    }
                }
            }

            // Create empty key map as the default one if none found.
            if (this.DefaultKeyMap == null)
            {
                this.DefaultKeyMap = new KeyMap();
            }
        }
    }
}
