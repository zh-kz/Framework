using UnityEngine;

namespace Framework.Character
{
    /// <summary>
    /// 角色状态基类
    /// </summary>
    public abstract class CharacterStatus : CachedBehaviour, IDamagable
    {
        /// <summary>
        /// 攻击距离
        /// </summary>
        public int attackDistance;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public int attackSpeed;

        /// <summary>
        /// 基础伤害
        /// </summary>
        public int baseDamage;

        /// <summary>
        /// 基础防御
        /// </summary>
        public int baseDefence;

        /// <summary>
        /// 当前基础生命值
        /// </summary>
        public int curBaseHealth;       // TODO: 更好的命名

        /// <summary>
        /// 基础最大生命值
        /// </summary>
        public int maxBaseHealth;

        /// <summary>
        /// 当前基础魔法值
        /// </summary>
        public int curBaseMana;

        /// <summary>
        /// 最大魔法值
        /// </summary>
        public int maxBaseMana;

        /// <summary>
        /// 受击特效点
        /// </summary>
        public Transform hitFxTF
        {
            get;
            protected set;
        }

        public virtual void OnDamage(int damage)
        {
            damage -= baseDefence;//暂时不考虑更多
            if(damage > 0)
            {
                curBaseHealth -= damage;
            }
            if(curBaseHealth <= 0)
            {
                Dead();
            }
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public abstract void Dead();

        protected virtual void Start()
        {
            //找到受击特效点
            hitFxTF = transform.Find("HitFxPos");
            if(hitFxTF == null)
            {
                hitFxTF = transform;
            }
        }
    }
}
