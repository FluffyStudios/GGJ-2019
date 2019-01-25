using System.Collections;

namespace FluffyBox
{
    public interface IKeyMappingService : IService
    {
        KeyMap ActiveKeyMap
        {
            get;
            set;
        }

        bool GetKeyAction(string actionName);

        bool GetKeyActionDown(string actionName);

        bool GetKeyActionUp(string actionName);

        float GetKeyActionAxis(string actionName, bool inverted = false);
    }
}
