using UnityEngine.InputSystem;

namespace N5.Toolbox
{
    public abstract class PlayerInputExtended<T> : PlayerInput
        where T : class, IInputActionCollection2, System.IDisposable, new()
    {
        public new T actions { get; private set; }

        /// <summary>
        /// Override this to point to <see cref="actions"/>.asset
        /// </summary>
        protected abstract InputActionAsset asset { get; }

        protected virtual void Awake()
        {
            actions = new T();
            base.actions = asset;
        }

        protected virtual void OnDestroy()
        {
            if (actions != null)
            {
                actions.Disable();
                actions.Dispose();
                actions = null;
            }
        }
    }

#if UNITY_EDITOR
    namespace Editor
    {
        using UnityEditor;
        using UnityEditor.UIElements;
        using UnityEngine.UIElements;
        using UnityEngine.InputSystem.Editor;

        [CustomEditor(typeof(PlayerInputExtended<>), true)]
        internal class PlayerInputExtendedEditor : Editor
        {
            private Editor _baseEditor;

            public override VisualElement CreateInspectorGUI()
            {
                CreateCachedEditor(targets, typeof(PlayerInputEditor), ref _baseEditor);

                VisualElement root = new VisualElement();

                HelpBox warning = new HelpBox(
                    "'Actions' field will be ignored and instead set automatically at runtime.",
                    HelpBoxMessageType.Warning
                );
                root.Add(warning);

                InspectorElement inspector = new InspectorElement(_baseEditor);
                root.Add(inspector);

                return root;
            }
        }
    }
#endif
}
