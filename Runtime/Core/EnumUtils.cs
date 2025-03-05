namespace N5.Toolbox
{
    public static class EnumUtils<T> where T : struct, System.Enum
    {
        private static readonly string[] s_names;
        private static readonly T[] s_values;

        public static System.ReadOnlySpan<string> Names => s_names;
        public static System.ReadOnlySpan<T> Values => s_values;

        static EnumUtils()
        {
            s_names = System.Enum.GetNames(typeof(T));

            System.Array values = System.Enum.GetValues(typeof(T));
            s_values = new T[values.Length];
            for (int i = 0; i < s_values.Length; i++)
            {
                s_values[i] = (T)values.GetValue(i);
            }
        }
    }
}
