using UnityEngine;

namespace UniOwl
{
    public class GameObjectPool : PoolBase<GameObject>
    {
        public GameObjectPool(GameObject prefab) : this(prefab, 0)
        {
            
        }
        
        public GameObjectPool(GameObject prefab, int initialCount) : base(() => PreloadFunction(prefab), GetAction, ReturnAction, initialCount)
        {
            
        }
        
        public GameObject Get(Transform parent)
        {
            return Get(Vector3.zero, Quaternion.identity, parent);
        }

        public GameObject Get(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var item = Get();
            var tr = item.transform;
            tr.SetParent(parent);
            tr.SetLocalPositionAndRotation(position, rotation);
            return item;
        }

        private static GameObject PreloadFunction(GameObject prefab) => Object.Instantiate(prefab);

        private static void GetAction(GameObject item) => item.SetActive(true);

        private static void ReturnAction(GameObject item) => item.SetActive(false);
    }
}