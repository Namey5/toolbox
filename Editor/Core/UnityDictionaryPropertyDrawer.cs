using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace N5.Toolbox.Editor
{
    [CustomPropertyDrawer(typeof(UnityDictionary<,>))]
    public class UnityDictionaryPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty entries = property.FindPropertyRelative("_entries");
            ListView listView = new ListView
            {
                headerTitle = property.displayName,
                showFoldoutHeader = true,
                showBorder = true,
                showAddRemoveFooter = true,
                showAlternatingRowBackgrounds = AlternatingRowBackground.All,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                makeItem = () =>
                {
                    PropertyField field = new PropertyField();
                    field.style.paddingLeft = 4f;
                    field.style.paddingRight = 6f;
                    return field;
                },
            };
            listView.style.maxHeight = 500f;
            listView.BindProperty(entries);
            root.Add(listView);

            return root;
        }
    }
}
