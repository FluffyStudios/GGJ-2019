using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FluffyBox
{
    [CustomEditor(typeof(GuiObject))]
    public class GuiObjectInspector : Editor
    {
        private string[] classNames;
        private string[] propertyNames;

        private GuiModifier currentPickedGuiModifier;

        public override void OnInspectorGUI()
        {
            GuiObject guiObject = (GuiObject)target;

            this.DrawBasics(guiObject);

            EditorGUILayout.Space();

            this.DrawModifiers(guiObject);

            EditorUtility.SetDirty(target);
        }

        private void DrawBasics(GuiObject target)
        {
            GUI.enabled = false;
            {
                target.Visible = EditorGUILayout.Toggle("Visible", target.Visible);
            }
            GUI.enabled = true;

            target.Alpha = EditorGUILayout.Slider("Alpha", target.Alpha, 0f, 1f);
        }

        private void DrawModifiers(GuiObject target)
        {
            EditorGUILayout.LabelField("Modifiers", EditorStyles.boldLabel);

            // Add Gui Modifier using Asset Selector.
            if (GUILayout.Button("Add Modifier"))
            {
                EditorGUIUtility.ShowObjectPicker<GuiModifier>(null, false, string.Empty, 0);
            }

            if (Event.current.commandName == "ObjectSelectorUpdated")
            {
                this.currentPickedGuiModifier = (GuiModifier)EditorGUIUtility.GetObjectPickerObject();
            }

            if (Event.current.commandName == "ObjectSelectorClosed" && this.currentPickedGuiModifier != null)
            {
                target.GuiModifiers.Add(this.currentPickedGuiModifier);
                this.currentPickedGuiModifier = null;
            }

            // Clean GuiModifiers if needed
            foreach (GuiModifier guiModifier in target.GuiModifiers)
            {
                if (guiModifier == null)
                {
                    target.GuiModifiers.Remove(guiModifier);
                }
            }

            // Draw the modifiers.
            for (int i = 0; i < target.GuiModifiers.Count; i++)
            {
                this.DrawModifier(target, target.GuiModifiers[i]);
            }
        }

        private void DrawModifier(GuiObject target, GuiModifier guiModifier)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Modifier Name", guiModifier.name, EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Target Property", guiModifier.TargetProperty, EditorStyles.boldLabel);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Value", guiModifier.StartValue + " → " + guiModifier.EndValue, EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Duration", guiModifier.Duration + (guiModifier.Loop ? " (Loop)" : string.Empty), EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Delay", guiModifier.StartDelay.ToString(), EditorStyles.boldLabel);

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Select"))
                        {
                            Selection.activeObject = guiModifier;
                        }                  

                        if (GUILayout.Button("Up ↑"))
                        {
                            int currentIndex = target.GuiModifiers.IndexOf(guiModifier);
                            if (currentIndex - 1 >= 0)
                            {
                                target.GuiModifiers.RemoveAt(currentIndex);
                                target.GuiModifiers.Insert(currentIndex - 1, guiModifier);
                            }
                        }

                        if (GUILayout.Button("Down ↓"))
                        {
                            int currentIndex = target.GuiModifiers.IndexOf(guiModifier);
                            if (currentIndex + 1 < target.GuiModifiers.Count)
                            {
                                target.GuiModifiers.RemoveAt(currentIndex);
                                target.GuiModifiers.Insert(currentIndex + 1, guiModifier);
                            }
                        }
                        
                        if (GUILayout.Button("Remove"))
                        {
                            target.GuiModifiers.Remove(guiModifier);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}