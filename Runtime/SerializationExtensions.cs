using System.IO;
using UnityEngine;

namespace N5.Toolbox
{
    public static class SerializationExtensions
    {
        public static void Write(this BinaryWriter self, Vector2 value)
        {
            self.Write(value.x);
            self.Write(value.y);
        }

        public static Vector2 ReadVector2(this BinaryReader self)
        {
            return new Vector2(self.ReadSingle(), self.ReadSingle());
        }

        public static void Write(this BinaryWriter self, Vector2Int value)
        {
            self.Write(value.x);
            self.Write(value.y);
        }

        public static Vector2Int ReadVector2Int(this BinaryReader self)
        {
            return new Vector2Int(self.ReadInt32(), self.ReadInt32());
        }

        public static void Write(this BinaryWriter self, Vector3 value)
        {
            self.Write(value.x);
            self.Write(value.y);
            self.Write(value.z);
        }

        public static Vector3 ReadVector3(this BinaryReader self)
        {
            return new Vector3(self.ReadSingle(), self.ReadSingle(), self.ReadSingle());
        }

        public static void Write(this BinaryWriter self, Vector3Int value)
        {
            self.Write(value.x);
            self.Write(value.y);
            self.Write(value.z);
        }

        public static Vector3Int ReadVector3Int(this BinaryReader self)
        {
            return new Vector3Int(self.ReadInt32(), self.ReadInt32(), self.ReadInt32());
        }

        public static void Write(this BinaryWriter self, Vector4 value)
        {
            self.Write(value.x);
            self.Write(value.y);
            self.Write(value.z);
            self.Write(value.w);
        }

        public static Vector4 ReadVector4(this BinaryReader self)
        {
            return new Vector4(self.ReadSingle(), self.ReadSingle(), self.ReadSingle(), self.ReadSingle());
        }

        public static void Write(this BinaryWriter self, Quaternion value, bool compress = false)
        {
            if (compress)
            {
                value.Normalize();
                self.Write((byte)(value.x * 127.5f + 127.5f));
                self.Write((byte)(value.y * 127.5f + 127.5f));
                self.Write((byte)(value.z * 127.5f + 127.5f));
                self.Write((byte)(value.w * 127.5f + 127.5f));
            }
            else
            {
                self.Write(value.x);
                self.Write(value.y);
                self.Write(value.z);
                self.Write(value.w);
            }
        }

        public static Quaternion ReadQuaternion(this BinaryReader self, bool compressed = false)
        {
            if (compressed)
            {
                return new Quaternion(
                    (self.ReadByte() - 127.5f) / 127.5f,
                    (self.ReadByte() - 127.5f) / 127.5f,
                    (self.ReadByte() - 127.5f) / 127.5f,
                    (self.ReadByte() - 127.5f) / 127.5f
                ).normalized;
            }
            else
            {
                return new Quaternion(self.ReadSingle(), self.ReadSingle(), self.ReadSingle(), self.ReadSingle());
            }
        }

        public static void Write(this BinaryWriter self, Color value)
        {
            self.Write(value.r);
            self.Write(value.g);
            self.Write(value.b);
            self.Write(value.a);
        }

        public static Color ReadColor(this BinaryReader self)
        {
            return new Color(self.ReadSingle(), self.ReadSingle(), self.ReadSingle(), self.ReadSingle());
        }

        public static void Write(this BinaryWriter self, Color32 value)
        {
            self.Write(value.r);
            self.Write(value.g);
            self.Write(value.b);
            self.Write(value.a);
        }

        public static Color32 ReadColor32(this BinaryReader self)
        {
            return new Color32(self.ReadByte(), self.ReadByte(), self.ReadByte(), self.ReadByte());
        }
    }
}
