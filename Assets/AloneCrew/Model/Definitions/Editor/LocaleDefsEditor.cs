using AloneCrew.Model.Definitions;
using AloneCrew.Model.Definitions.Localization;
using AloneCrew.Utils.Editor;
using UnityEditor;

namespace AloneCrew.Components.Dialogs.Editor
{
    [CustomEditor(typeof(LocaleDef))]
    public class LocaleDefsEditor : UnityEditor.Editor
    {
        private SerializedProperty _modeProperty;

        private void OnEnable()
        {
            _modeProperty = serializedObject.FindProperty("_mode");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_modeProperty);
            LocaleDef.LocaleDefMode mode;
            if (_modeProperty.GetEnum(out mode))
            {
                switch (mode)
                {
                    case LocaleDef.LocaleDefMode.File: EditorGUILayout.PropertyField(serializedObject.FindProperty("_tsvFile")); break;
                    case LocaleDef.LocaleDefMode.Url: EditorGUILayout.PropertyField(serializedObject.FindProperty("_url")); break;

                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_locales"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}