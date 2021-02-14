using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    /// UI窗口基类：定义所有窗口共有成员（暂时只有显隐）
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIWindow : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        // 事件监听器缓存
        private Dictionary<string, UIEventListener> uiEventMap;

        // 需要设置脚本执行顺序，否则可能manager先awake
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            uiEventMap = new Dictionary<string, UIEventListener>();
        }

        /// <summary>
        /// 设置窗口可见性
        /// </summary>
        /// <param name="state">显隐状态</param>
        /// <param name="delay">延迟时间</param>
        public void SetVisible(bool state, float delay = 0)
        {
            StartCoroutine(SetVisibleDelay(state, delay));
        }

        /// <summary>
        /// 获取子UI元素上的事件监听器
        /// </summary>
        /// <param name="name">子UI元素名</param>
        /// <returns></returns>
        public UIEventListener GetUIEventListener(string name)
        {
            // 使用缓存
            UIEventListener value;
            if(!uiEventMap.TryGetValue(name, out value))
            {
                Transform child = transform.FindChildByName(name);
                if(child == null)
                {
                    Debug.LogError("Can't find child: " + name);
                    return null;
                }
                // 这里的value还是有可能为null
                // 使用UIEventListener提供的方法，保证找到
                uiEventMap.Add(name, value = UIEventListener.GetListener(child));
            }
            return value;
        }

        private IEnumerator SetVisibleDelay(bool state, float delay)
        {
            if(delay != 0)
            {
                yield return Waits.GetWaitForSeconds(delay);
            }
            canvasGroup.alpha = state ? 1 : 0;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
    
    }
}
