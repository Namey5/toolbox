using UnityEngine.UIElements;

namespace N5.Toolbox
{
    public partial class EditorDialog
    {
        public VisualElement root => _root;
        public event System.Action OnClose
        {
            add => _onClose += value;
            remove => _onClose -= value;
        }

        public partial void Close();
    }

#if UNITY_EDITOR
    public partial class EditorDialog
    {
        private readonly Window _window;

        public EditorDialog()
        {
            _window = UnityEditor.EditorWindow.CreateWindow<Window>();
        }

        private VisualElement _root => _window.rootVisualElement;
        private event System.Action _onClose { add => _window.OnClose += value; remove => _window.OnClose -= value; }

        public partial void Close() => _window.Close();

        private class Window : UnityEditor.EditorWindow
        {
            public event System.Action OnClose;

            private void OnDestroy()
            {
                OnClose?.Invoke();
            }
        }
#else
        public partial class EditorDialog
        {
            private VisualElement _root = new VisualElement();
            private event System.Action _onClose;

            public partial void Close() { }
        }
#endif
    }
}
