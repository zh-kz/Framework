using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;
using Framework.UI;
using UnityEngine.EventSystems;

namespace Test
{
    /// <summary>
    /// 游戏主窗口
    /// </summary>
    public class UIMainWindow : UIWindow
    {
        // 1.在开始时注册需要交互的UI元素
        private void Start()
        {
            GetUIEventListener("ButtonGameStart").PointerClick += OnPointerClick;
        }

        // 2.提供面板负责的交互行为
        private void OnPointerClick(PointerEventData eventData)
        {
            print(eventData.pointerPress + "--" + eventData.clickCount);
            GameController.Instance.GameStart();
        }

    }
}
