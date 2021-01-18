using UnityEngine;

namespace Framework
{
    /// <summary>
    /// MonoBehaviour单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : CachedBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        /// <summary>
        /// 唯一实例
        /// </summary>
        /// <value></value>
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    var tmp = FindObjectsOfType(typeof(T));
                    if(tmp == null || tmp.Length == 0)
                    {
                        Debug.LogError("No instance finded");
                        return null;
                    }
                    else if(tmp.Length > 1)
                    {
                        Debug.LogError("More than one instance finded");
                    }
                    instance = tmp[0] as T;
                }
                return instance;
            }
        }
    }
}
