using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// 角色动画控制
    /// </summary>
    public class CharacterAnimation : CachedBehaviour
    {
        // TODO:动画参数结构
        public static readonly int hashSpeed = Animator.StringToHash("Speed");

        private Animator animator;
        private int preHashAnimName;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            animator.applyRootMotion = false;
        }

        /// <summary>
        /// 播放动画（bool）
        /// </summary>
        /// <param name="hashAnimName"></param>
        public void PlayAnimation(int hashAnimName)
        {
            animator.SetBool(preHashAnimName, false);
            animator.SetBool(hashAnimName, true);
            preHashAnimName = hashAnimName;
        }

        /// <summary>
        /// 设置动画浮点值
        /// </summary>
        /// <param name="hashAnimName"></param>
        /// <param name="value"></param>
        public void SetFloat(int hashAnimName, float value)
        {
            animator.SetFloat(hashAnimName, value);
        }

        /// <summary>
        /// 设置bool值
        /// </summary>
        /// <param name="hashAnimName"></param>
        /// <param name="value"></param>
        public void SetBool(int hashAnimName, bool value)
        {
            animator.SetBool(hashAnimName, value);
        }

        /// <summary>
        /// 设置触发
        /// </summary>
        /// <param name="hashAnimName"></param>
        public void SetTrigger(int hashAnimName)
        {
            animator.SetTrigger(hashAnimName);
        }

    }
}
