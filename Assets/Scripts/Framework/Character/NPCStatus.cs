using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// NPC状态信息，TODO: 与FSM结合
    /// </summary>
    public class NPCStatus : CharacterStatus
    {
        /// <summary>
        /// 贡献经验值
        /// </summary>
        public int offerExp;

        public override void Dead()
        {
            Destroy(gameObject, 2);
        }

        public override void OnDamage(int damage)
        {
            curBaseHealth -= damage - baseDefence;
            if(curBaseHealth <= 0)
            {
                Dead();
                curBaseHealth = 0;
            }
        }

        private void OnDestroy()
        {
            Debug.Log("NPC Destroyed");
        }
    }
}
