using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Character;

namespace Framework.Skill
{
    /// <summary>
    /// 技能管理器，实例化/准备/调用是放弃，cooldown
    /// </summary>
    public class SkillManager : CachedBehaviour
    {
        /// <summary>
        /// 此角色所有的技能数据
        /// </summary>
        public SkillData[] skills;

        /// <summary>
        /// 所属角色的状态
        /// </summary>
        private CharacterStatus characterStatus;

        private void Start()
        {
            if(skills == null)
            {
                Debug.LogError("This Character has no skill data, CharacterSkillManager should be removed.");
                enabled = false;
                Destroy(this);
                return;
            }
            
            //初始化各技能数据
            foreach(var skill in skills)
            {
                if(!string.IsNullOrEmpty(skill.prefabName) && skill.skillPrefab == null)
                {
                    skill.skillPrefab = LoadPreafb(skill.prefabName);
                }
                if(!string.IsNullOrEmpty(skill.hitFxName) && skill.hitFxPrefab == null)
                {
                    skill.hitFxPrefab = LoadPreafb(skill.hitFxName);
                }
                skill.owner = gameObject;
                skill.hashAnimationName = Animator.StringToHash(skill.animationName);
            }

            characterStatus = GetComponent<CharacterStatus>();
        }

        /// <summary>
        /// 准备id对应的技能，检查能否释放
        /// </summary>
        /// <param name="skillId">技能id</param>
        /// <returns>id对应的技能数据</returns>
        public SkillData PrepareSkill(SkillID skillId)
        {
            for(int i = skills.Length - 1; i >= 0; i--)
            {
                if(skills[i] != null && skills[i].id == skillId)
                {
                    if(skills[i].coolRemain <= 0
                        && skills[i].costMana <= characterStatus.curBaseMana)
                    {
                        return skills[i];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="skillData">技能数据</param>
        public void DeploySkill(SkillData skillData)
        {
            var tempGo = GameObjectPool.Instance.CreateObject(
                skillData.prefabName, skillData.skillPrefab, transform.position, transform.rotation
                );
            var deployer = tempGo.GetComponent<SkillDeployer>();       
            deployer.SkillData = skillData;                            
            deployer.DeploySkill();

            StartCoroutine(CoolDown(skillData));
        }

        /// <summary>
        /// 加载预制体资源
        /// </summary>
        /// <param name="name">预制体名</param>
        /// <returns></returns>
        private GameObject LoadPreafb(string name)
        {
            var prefabGo = ResourceManager.Load<GameObject>(name);
            var tempGo = GameObjectPool.Instance.CreateObject(
                name, prefabGo, transform.position, transform.rotation
                );
            GameObjectPool.Instance.CollectObject(tempGo);
            
            return prefabGo;
        }

        private IEnumerator CoolDown(SkillData skillData)
        {
            skillData.coolRemain = skillData.coolTime;
            while(skillData.coolRemain > 0)
            {
                yield return Waits.wait1_0s;
                skillData.coolRemain--;
            }
            skillData.coolRemain = 0;
        }
    }
}
