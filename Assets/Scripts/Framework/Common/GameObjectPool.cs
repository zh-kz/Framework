using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 对象池
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        // 类型 -> 一类对象
        private Dictionary<string, List<GameObject>> cache;

        private void Awake()
        {
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="key">对象关键名</param>
        /// <param name="prefab">预制体</param>
        /// <param name="position">实例位置</param>
        /// <param name="rotation">实例旋转</param>
        /// <param name="parent">实例父物体</param>
        /// <returns>实例</returns>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)    //考虑扩展设置父物体
        {
            GameObject go;
            if(cache.ContainsKey(key))
            {
                if((go = cache[key].Find(t => !t.activeInHierarchy)) != null)   //有未激活的（可使用）
                {
                    go.transform.position = position;
                    go.transform.rotation = rotation;
                    go.SetActive(true);
                }
                else
                {
                    go = Instantiate(prefab, position, rotation, parent == null ? transform : parent);  //实例化并作为对象池的子物体
                    go.name = key;
                    cache[key].Add(go);
                }
            } 
            else
            {
                cache.Add(key, new List<GameObject>());
                go = Instantiate(prefab, position, rotation, parent == null ? transform : parent);  //实例化并作为对象池的子物体
                go.name = key;
                cache[key].Add(go);
            }

            return go;
        }

        /// <summary>
        /// 删除一类对象
        /// </summary>
        /// <param name="key">对象关键字</param>
        public void Clear(string key)
        {
            if(cache.ContainsKey(key))
            {
                cache[key].ForEach(go => Destroy(go));
                cache.Remove(key);
            }
        }

        /// <summary>
        /// 删除所有对象
        /// </summary>
        public void ClearAll()
        {
            foreach(var key in cache.Keys)
            {
                cache[key].ForEach(go => Destroy(go));
            }
            cache.Clear();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="go">对象</param>
        public void CollectObject(GameObject go)
        {
            // TODO: 考虑go不在pool中时??
            go.SetActive(false);        
        }

        /// <summary>
        /// 延迟回收
        /// </summary>
        /// <param name="go">对象</param>
        /// <param name="delay">延迟时间</param>
        public void CollectObject(GameObject go, float delay)
        {
            StartCoroutine(CollectDelay(go, delay));
        }

        private IEnumerator CollectDelay(GameObject go, float delay)
        {
            yield return Waits.GetWaitForSeconds(delay);

            CollectObject(go);
        }
    }
}
