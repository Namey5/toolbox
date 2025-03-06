using UnityEngine;

namespace N5.Toolbox
{
    public class CustomGuiAttribute : PropertyAttribute
    {
        public readonly string createGuiMethodName;

        public CustomGuiAttribute(string createGuiMethodName)
        {
            this.createGuiMethodName = createGuiMethodName;
        }
    }
}
