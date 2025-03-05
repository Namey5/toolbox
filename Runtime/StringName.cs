using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace N5.Toolbox
{
    public readonly struct StringName : System.IEquatable<StringName>
    {
        private const int DefaultRegistrySize = 128;

        private static readonly List<string> s_nameRegistry = new List<string>(DefaultRegistrySize);
        private static readonly Dictionary<string, int> s_idRegistry = new Dictionary<string, int>(DefaultRegistrySize);

        public readonly int id;

        public StringName(string name)
        {
            if (!s_idRegistry.TryGetValue(name, out id))
            {
                id = s_nameRegistry.Count;
                s_nameRegistry.Add(name);
                s_idRegistry.Add(name, id);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(StringName other) => other.id == id;

        public bool IsValid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => id > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(StringName left, StringName right) => left.Equals(right);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(StringName left, StringName right) => !left.Equals(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(StringName name) => name.id;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator StringName(string name) => new StringName(name);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator string(StringName name) => name.ToString();

        [Unity.Burst.BurstDiscard]
        public override bool Equals(object obj) => obj is StringName name && Equals(name);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => id;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => s_nameRegistry[id];
    }

    [System.Serializable]
    public struct UnityStringName : ISerializationCallbackReceiver
    {
        [SerializeField] private string name;

        public StringName Value { get; private set; }

        public UnityStringName(string name)
        {
            this.name = name;
            Value = new StringName(name);
        }

        public UnityStringName(StringName value)
        {
            name = value.ToString();
            Value = value;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Value = new StringName(name);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        public static implicit operator UnityStringName(string name) => new UnityStringName(name);
        public static implicit operator UnityStringName(StringName name) => new UnityStringName(name);
        public static implicit operator StringName(UnityStringName name) => name.Value;
    }
}
