using System.Collections.Generic;
using UnityEngine;

namespace N5.Toolbox
{
    [System.Serializable]
    public struct UnityKeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;

        public UnityKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [System.Serializable]
    public class UnityDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<UnityKeyValuePair<TKey, TValue>> _entries = new List<UnityKeyValuePair<TKey, TValue>>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            EnsureCapacity(_entries.Count);
            foreach (UnityKeyValuePair<TKey, TValue> entry in _entries)
            {
                bool success = TryAdd(entry.key, entry.value);
                Debug.Assert(success, $"Key '{entry.key}' already exists in dictionary.");
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}
