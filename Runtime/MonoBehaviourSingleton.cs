using UnityEngine;

namespace N5.Toolbox
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviourSingleton<T>
    {
        protected static readonly string TypeName = typeof(T).Name;

        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            Debug.Assert(
                Instance == null,
                $"[MonoBehaviourSingleton<{TypeName}>] There is more than one instance in the scene!",
                this
            );

            Instance = (T)this;
        }
    }
}
