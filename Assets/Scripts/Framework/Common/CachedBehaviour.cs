using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 缓存transform和gameObject
    /// </summary>
    public class CachedBehaviour : MonoBehaviour
    {
        private Transform _transform;

        /// <summary>
        /// Cached transform
        /// </summary>
        /// <value></value>
        public new Transform transform
        {
            get => _transform == null ? _transform = base.transform : _transform;
        }

        private GameObject _gameObject;

        /// <summary>
        /// Cached gameObject
        /// </summary>
        /// <value></value>
        public new GameObject gameObject
        {
            get => _gameObject == null ? _gameObject = base.gameObject : _gameObject;
        }
    }
}
