using System.Collections.Generic;
using UnityEditor;

namespace UniOwl.Editor
{
    public static class EditorUtils
    {
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            property = property.Copy();

            SerializedProperty nextElement = property.Copy();
                
            if (!nextElement.NextVisible(false))
                nextElement = null;

            property.NextVisible(true);
                
            do
            {
                if (SerializedProperty.EqualContents(property, nextElement))
                    yield break;

                yield return property;

            } while (property.NextVisible(false));
        }
    }
}