using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Skill;

namespace Framework.Character
{
    /// <summary>
    /// 角色技能系统，封装系统框架，提供简单技能释放功能
    /// </summary>
    [RequireComponent(typeof(SkillManager))]
    public class CharacterSkillSystem : CachedBehaviour
    {
        /// <summary>
        /// 此角色的技能管理器
        /// </summary>
        private SkillManager skillManager;

        /// <summary>
        /// 当前攻击目标
        /// </summary>
        private Transform curTarget;
        /// <summary>
        /// 当前目标的CharacterSelected
        /// </summary>
        private CharacterSelected curAttackTargetSelected;

        /// <summary>
        /// 当前使用技能
        /// </summary>
        private SkillData curSkill;

        private CharacterAnimation characterAnimation;
        private CharacterStatus characterStatus;

        private void Start()
        {
            characterAnimation = GetComponent<CharacterAnimation>();
            GetComponentInChildren<AnimationEventBehaviour>().onAttack += DeployCurrentSKill;
            
            skillManager = GetComponent<SkillManager>();
            characterStatus = GetComponent<CharacterStatus>();
        }

        /// <summary>
        /// 使用指定id的技能
        /// </summary>
        /// <param name="skillId">技能id</param>
        /// <param name="isBatter">是否为连击</param>
        public void UseSkill(SkillID skillId, bool isBatter = false)
        {
            if(isBatter && curSkill != null)   
            {                                  
                skillId = curSkill.nextBatterId;
            }
            curSkill = skillManager.PrepareSkill(skillId);
            if(curSkill == null)
            {
                Debug.Log("Can't use: " + skillId.ToString());
                return;
            }

            if(curSkill.attackType == SkillAttackType.Single)
            {
                var target = SelectClosestTarget();     
                if(target == null)
                {
                    Debug.Log("Can't find target for: " + skillId.ToString());
                }
                else
                {
                    ShowSelectedFx(false);
                    curTarget = target;
                    curAttackTargetSelected = curTarget.GetComponent<CharacterSelected>();
                    ShowSelectedFx(true);
                    transform.LookAt(curTarget);
                }
            }
            else
            {
                // TODO
            }

            characterAnimation.SetTrigger(curSkill.hashAnimationName);  
        }

        /// <summary>
        /// 随机使用一个技能
        /// </summary>
        public void UseRandomSkill()
        {
            var allLen = skillManager.skills.Length;
            var usableSkillIndexs = new List<int>(allLen);    
            var curMana = characterStatus.curBaseMana;
            for(var i = allLen - 1; i >= 0; i--)
            {
                var skill = skillManager.skills[i];
                if(skill != null && skill.coolRemain == 0 && skill.costMana <= curMana)
                {
                    usableSkillIndexs.Add(i);
                }
            }
            if(usableSkillIndexs.Count > 0)
            {
                var index = Random.Range(0, usableSkillIndexs.Count);
                var skillId = skillManager.skills[usableSkillIndexs[index]].id;
                UseSkill(skillId, false);       
            }
        }

        /// <summary>
        /// 释放当前技能
        /// </summary>
        private void DeployCurrentSKill()
        {
            if(curSkill != null)
            {
                skillManager.DeploySkill(curSkill);
            }
        }

        /// <summary>
        /// 选择最近的目标
        /// </summary>
        /// <returns></returns>
        private Transform SelectClosestTarget()
        {
            List<GameObject> allTargets = new List<GameObject>(curSkill.attackTargetTags.Length);
            foreach(var tag in curSkill.attackTargetTags)
            {
                var targets = GameObject.FindGameObjectsWithTag(tag);
                if(targets != null && targets.Length > 0)
                {
                    allTargets.AddRange(targets);
                }
            }
            if(allTargets.Count == 0)
            {
                return null;
            }
            GameObject closestTarget = null;
            var dis2 = curSkill.attackDistance * curSkill.attackDistance;
            var minDis2 = float.MaxValue;
            var curDis2 = 0.0f;
            foreach(var t in allTargets)
            {
                if((curDis2 = (t.transform.position - transform.position).sqrMagnitude) < dis2      //暂时使用自身位置为释放点
                    && t.GetComponent<CharacterStatus>().curBaseHealth > 0)                         //暂时使用baseHelath
                {
                    if(curDis2 < minDis2)
                    {
                        minDis2 = curDis2;
                        closestTarget = t;
                    }
                }
            }

            return closestTarget == null ? null : closestTarget.transform;
        }

        /// <summary>
        /// 控制当前选中目标的选中效果的显隐
        /// </summary>
        /// <param name="show">是否显示</param>
        private void ShowSelectedFx(bool show)
        {
            if(curTarget != null)
            {
                curAttackTargetSelected.SetSelectedActive(show);
            }
        }

    }
}
