using UnityEngine;

namespace UniOwl
{
    public class ComponentPool<T> : PoolBase<T> where T : Component
    {
        public ComponentPool(T prefab, int initialCount) : base(() => PreloadFunction(prefab), GetAction, ReturnAction, initialCount)
        {
            
        }

        public T Get(Transform parent)
        {
            return Get(Vector3.zero, Quaternion.identity, parent);
        }

        public T Get(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            T item = Get();
            var tr = item.transform;
            tr.SetParent(parent);
            tr.SetLocalPositionAndRotation(position, rotation);
            return item;
        }
        
        private static T PreloadFunction(T prefab) => Object.Instantiate(prefab);

        private static void GetAction(T item) => item.gameObject.SetActive(true);

        private static void ReturnAction(T item) => item.gameObject.SetActive(false);
    }
}