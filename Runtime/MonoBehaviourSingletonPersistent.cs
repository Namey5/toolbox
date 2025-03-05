using UnityEngine;

namespace N5.Toolbox
{
    public abstract class MonoBehaviourSingletonPersistent<T> : MonoBehaviourSingleton<T>
        where T : MonoBehaviourSingletonPersistent<T>
    {
        public bool IsAutoCreatedInstance { get; private set; } = false;

        public static T FastInstance
        {
            get => MonoBehaviourSingleton<T>.Instance;
            protected set => MonoBehaviourSingleton<T>.Instance = value;
        }

        public static new T Instance
        {
            get
            {
                if (FastInstance != null)
                {
                    Debug.Log($"[MonoBehaviourSingletonPersistent<{TypeName}>] Auto creating instance...");
                    GameObject gameObject = new GameObject(TypeName + "_Instance")
                    {
                        hideFlags = HideFlags.HideAndDontSave,
                    };
                    FastInstance = gameObject.AddComponent<T>();
                    FastInstance.IsAutoCreatedInstance = true;
                }

                return FastInstance;
            }
        }

        protected override void Awake()
        {
            if (FastInstance != null)
            {
                if (FastInstance != this)
                {
                    Debug.LogError(
                        $"[MonoBehaviourSingletonPersistent<{TypeName}>] There is more than one instance in the scene!",
                        this
                    );
                    Destroy(this);
                }
                return;
            }

            FastInstance = (T)this;
        }
    }
}
