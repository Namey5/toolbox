using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace N5.Toolbox.Editor
{
    [CustomPropertyDrawer(typeof(CustomGuiAttribute))]
    public class CustomGuiAttributePropertyDrawer : PropertyDrawer
    {
        private CustomGuiAttribute GuiAttribute => (CustomGuiAttribute)attribute;

        private readonly object[] _params = new object[1];

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            Object target = property.serializedObject.targetObject;
            System.Type type = target.GetType();
            MethodInfo methodInfo = type.GetMethod(
                GuiAttribute.createGuiMethodName,
                BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance
                    | BindingFlags.Static);

            if (methodInfo != null)
            {
                _params[0] = property;
                root.Add((VisualElement)methodInfo.Invoke(
                    methodInfo.IsStatic ? null : target,
                    methodInfo.GetParameters().Length > 0 ? _params : null));
            }
            else
            {
                root.Add(new PropertyField(property));
            }

            return root;
        }
    }
}
