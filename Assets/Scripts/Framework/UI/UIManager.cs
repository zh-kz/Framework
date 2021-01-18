using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    /// <summary>
    /// UI管理器：管理(记录/隐藏)所有窗口，提供查找窗口方法
    /// </summary>
    public class UIManager : MonoSingleton<UIManager> 
    {
        // 窗口类型名(一个类型应该只有一个对象)=>UIWindow
        // 注意如果是多个实例的，应该作为窗口元素，而不是窗口
        private Dictionary<string, UIWindow> uiWindowMap;
        
        private void Awake()
        {
            UIWindow[] uIWindows = FindObjectsOfType<UIWindow>();
            uiWindowMap = new Dictionary<string, UIWindow>(uIWindows.Length);
            for(int i = uIWindows.Length - 1; i >= 0; i--)
            {
                // 隐藏
                uIWindows[i].SetVisible(false);
                // 记录
                uiWindowMap.Add(uIWindows[i].GetType().Name, uIWindows[i]);
            }
        }

        /// <summary>
        /// 根据类型查找窗口
        /// </summary>
        /// <typeparam name="T">窗口类型</typeparam>
        /// <returns></returns>
        public T GetWindow<T>() where T : UIWindow
        {
            string key = typeof(T).Name;
            UIWindow value;
            if(!uiWindowMap.TryGetValue(key, out value))
            {
                return null;
            }
            return value as T;
        }

        /// <summary>
        /// 添加新窗口（用于动态创建时）
        /// </summary>
        /// <param name="window"></param>
        public void AddWindow(UIWindow window)
        {
            uiWindowMap.Add(window.GetType().Name, window);
        }

    }
}
