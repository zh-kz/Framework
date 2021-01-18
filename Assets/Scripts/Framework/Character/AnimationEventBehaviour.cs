using System;
using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// 动画事件行为，定时事件绑定；挂载到模型Animatior上(这样只有模型时一样可以绑定事件)
    ///     注意拖入Animator编辑窗口的必须是原动画整体文件，而不是新建子状态后拖入动画片段，否则不会触发事件
    ///     记得apply
    /// </summary>
    public class AnimationEventBehaviour : CachedBehaviour
    {
        private Animator animator;

        /// <summary>
        /// 当动画曲线上到达OnAttack时间点时
        /// </summary>
        public event Action onAttack;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// 在撤销动画时
        /// </summary>
        /// <param name="hashAnimName"></param>
        public void OnCancelAnimation(int hashAnimName)
        {
            animator.SetBool(hashAnimName, false);
        }

        /// <summary>
        /// 绑定在动画曲线上，定时触发
        /// </summary>
        public void OnAttack()
        {
            Debug.Log("OnAttack");
            if(onAttack != null)
            {
                onAttack();
            }
        }

    }
}
