using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using Framework.UI;

namespace Test
{
    /// <summary>
    /// 游戏控制器：负责处理游戏流程
    /// </summary>
    public class GameController : MonoSingleton<GameController>
    {
        // 游戏开始前
        private void Start()
        {
            // 显示主窗口
            UIManager.Instance.GetWindow<UIMainWindow>().SetVisible(true);
        }
        
        // 游戏开始
        public void GameStart()
        {
            UIManager.Instance.GetWindow<UIMainWindow>().SetVisible(false);
        }

        // 游戏结束

        // 游戏暂停
    }
}
