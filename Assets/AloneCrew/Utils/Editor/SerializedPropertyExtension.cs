using System;
using UnityEditor;

namespace AloneCrew.Utils.Editor
{
    public static class SerializedPropertyExtension
    {
        public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType enumValue) where TEnumType : Enum
        {
            enumValue = default;
            var names = property.enumNames;
            if (names == null || names.Length == 0)
                return false;
            var enumName = names[property.enumValueIndex];
            enumValue = (TEnumType) Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
        
    }
}