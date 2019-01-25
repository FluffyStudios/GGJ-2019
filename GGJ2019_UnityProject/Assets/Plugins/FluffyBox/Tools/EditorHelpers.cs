using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace FluffyBox
{
    public class EditorHelpers
    {
        public static readonly GUILayoutOption DefaultLabelSize = GUILayout.Width(100f);
        public static readonly GUILayoutOption DefaultFloatFieldSize = GUILayout.Width(48f);
        public static readonly GUILayoutOption DefaultIntFieldSize = GUILayout.Width(48f);

        private static EditorGUIColorBlock colorBlock = new EditorGUIColorBlock();

        public static EditorGUIColorBlock ColorBlock(Color color)
        {
            return colorBlock.Begin(color);
        }
        
        public static Type GetType(string typeName)
        {
            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, etc.
            Type type = Type.GetType(typeName);

            // If it worked, then we're done here
            if (type != null)
            {
                return type;
            }

            // Get the name of the assembly (Assumption is that we are using
            // fully-qualified type names)
            if (!typeName.Contains("."))
            {
                return null;
            }

            string assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

            // Attempt to load the indicated Assembly
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                return null;
            }

            // Ask that assembly to return the proper Type
            return assembly.GetType(typeName);
        }

        public class EditorGUIColorBlock : IDisposable
        {
            private Color prevColor;

            public EditorGUIColorBlock Begin(Color newColor)
            {
                prevColor = GUI.color;
                GUI.color = newColor;
                return this;
            }

            public void Dispose()
            {
                GUI.color = prevColor;
            }
        }
    }
}
