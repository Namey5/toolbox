using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace N5.Toolbox.Editor
{
    [CustomPropertyDrawer(typeof(UnityStringName))]
    public class StringNamePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("name"), property.displayName);
        }
    }
}
