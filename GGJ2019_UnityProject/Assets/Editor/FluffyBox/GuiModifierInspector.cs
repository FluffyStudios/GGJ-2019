using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FluffyBox
{
    [CustomEditor(typeof(GuiModifier))]
    public class GuiModifierInspector : Editor
    {
        private string[] propertyNames;

        public override void OnInspectorGUI()
        {
            GuiModifier guiModifier = (GuiModifier)target;
            
            this.BuildPropertyNames();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.LabelField("Target Property", EditorStyles.boldLabel);

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Start Value");
                    EditorGUILayout.LabelField("End Value");
                    EditorGUILayout.LabelField("Duration");
                    EditorGUILayout.LabelField("Start Delay");
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                {
                    int currentTargetPropertyIndex = Mathf.Clamp(Array.IndexOf(this.propertyNames, guiModifier.TargetProperty), 0, this.propertyNames.Length - 1);
                    guiModifier.TargetProperty = this.propertyNames[EditorGUILayout.Popup(currentTargetPropertyIndex, this.propertyNames, GUILayout.Width(128f))];

                    EditorGUILayout.Space();

                    guiModifier.StartValue = EditorGUILayout.FloatField(guiModifier.StartValue, EditorHelpers.DefaultFloatFieldSize);
                    guiModifier.EndValue = EditorGUILayout.FloatField(guiModifier.EndValue, EditorHelpers.DefaultFloatFieldSize);

                    EditorGUILayout.BeginHorizontal();
                    {
                        guiModifier.Duration = Mathf.Clamp(EditorGUILayout.FloatField(guiModifier.Duration, EditorHelpers.DefaultFloatFieldSize), 0f, float.PositiveInfinity);
                        guiModifier.Loop = EditorGUILayout.ToggleLeft("Loop", guiModifier.Loop);
                    }
                    EditorGUILayout.EndHorizontal();

                    guiModifier.StartDelay = Mathf.Clamp(EditorGUILayout.FloatField(guiModifier.StartDelay, EditorHelpers.DefaultFloatFieldSize), 0f, float.PositiveInfinity);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField(" Animation Curve", EditorStyles.boldLabel);
            guiModifier.AnimationCurve = EditorGUILayout.CurveField(guiModifier.AnimationCurve, GUILayout.Height(96f));

            EditorUtility.SetDirty(target);
        }
        
        private void BuildPropertyNames()
        {
            PropertyInfo[] properties = typeof(GuiObject).GetProperties();
            List<string> propertyNameList = new List<string>();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType == typeof(float))
                {
                    propertyNameList.Add(properties[i].Name);
                }
            }

            this.propertyNames = propertyNameList.ToArray();
        }
    }
}