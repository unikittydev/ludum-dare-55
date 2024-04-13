using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UniOwl
{
    public abstract class ScriptableStructObject<T> : ScriptableObject where T : unmanaged
    {
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set
            {
                #if UNITY_EDITOR
                Undo.RecordObject(this, $"Set: {nameof(T)}");
                #endif

                _value = value;
                
                #if UNITY_EDITOR
                EditorUtility.SetDirty(this);
                #endif
            }
        }

        public static implicit operator T(ScriptableStructObject<T> so) => so._value;
    }
}