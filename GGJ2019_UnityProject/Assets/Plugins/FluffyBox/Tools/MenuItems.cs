using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FluffyBox
{
    public class MenuItems : MonoBehaviour
    {
        [MenuItem("GameObject/FluffyBox/GuiObject", false, 10)]
        static void CreateGuiObject(MenuCommand menuCommand)
        {
            // Create a custom game object
            GameObject gameObject = new GameObject("NewGuiObject", typeof(RectTransform), typeof(CanvasRenderer), typeof(CanvasGroup), typeof(GuiObject));
            
            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
            Selection.activeObject = gameObject;
        }
    }
}
