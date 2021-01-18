using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Skill
{
    /// <summary>
    /// 技能释放器基类:选区，影响，回收
    /// </summary>
    public abstract class SkillDeployer : CachedBehaviour
    {
        /// <summary>
        /// 此释放器所属的技能数据
        /// </summary>
        protected SkillData skillData;        //使用private的话派生类不能访问
        /// <summary>
        /// 此技能的释放器
        /// </summary>
        protected IAttackSelector attackSelector;
        /// <summary>
        /// 此技能造成的一系列自身影响
        /// </summary>
        protected List<ISelfImpact> selfImpacts;      //SkillData中使用的是数组，而这里是列表，需要统一？
        /// <summary>
        /// 此技能造成的一系列目标影响
        /// </summary>
        protected List<ITargetImpact> targetImpacts;

        /// <summary>
        /// 此技能释放器所属的技能数据
        /// </summary>
        /// <value></value>
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                if(value != null)
                {
                    skillData = value;
                    //创建选择器
                    attackSelector = DeployerConfigFactory.CreateAttackSelector(skillData);
                    //创建自身影响
                    selfImpacts = DeployerConfigFactory.CreateSelfImpacts(skillData);
                    //创建目标影响
                    targetImpacts = DeployerConfigFactory.CreateTargetImpacts(skillData);
                }
            }
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        public abstract void DeploySkill();

        /// <summary>
        /// 根据技能释放器计算目标
        /// </summary>
        /// <returns>目标TF数组，没有目标则返回null</returns>
        public Transform[] CalculateTargets()
        {
            var targets = attackSelector.SelectTarget(skillData, transform);
            return targets != null && targets.Length > 0 ? targets : null;
        }

        /// <summary>
        /// 回收技能对象
        /// </summary>
        public void CollectSkill()
        {
            if(skillData.durationTime > 0)
            {
                GameObjectPool.Instance.CollectObject(gameObject, skillData.durationTime);  //回收的是这个技能预制体的实例gameobject
            }
            else
            {
                GameObjectPool.Instance.CollectObject(gameObject, 0.2f); 
            }
        }

    }
}
