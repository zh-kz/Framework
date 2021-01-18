using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// Player状态
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {
        /// <summary>
        /// 当前经验值
        /// </summary>
        public int curExp;
        /// <summary>
        /// 最大经验值
        /// </summary>
        public int maxExp;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int curLevel;
        /// <summary>
        /// 最大等级
        /// </summary>
        public int maxLevel;

        /// <summary>
        /// 获取经验值
        /// </summary>
        public void GetExp()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 升级
        /// </summary>
        public void LevelUp()
        {
            throw new System.NotImplementedException();
        }

        public override void Dead()
        {
            Debug.Log("Player dead");
        }

        public override void OnDamage(int damage)
        {
            Debug.Log("Player on damage");
        }
    }
}
