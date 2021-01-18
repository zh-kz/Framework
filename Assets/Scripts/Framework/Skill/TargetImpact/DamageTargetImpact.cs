using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Character;

namespace Framework.Skill
{
    /// <summary>
    /// 伤害目标
    /// </summary>
    public class DamageTargetImpact : ITargetImpact
    {
        public void ImpactTarget(SkillDeployer deployer)
        {
            var characterStatus = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            if(characterStatus == null)
            {
                return;
            }
            deployer.StartCoroutine(RepeatDamage(deployer, characterStatus));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer, CharacterStatus characterStatus)
        {
            float attackedTime = 0;
            var skillData = deployer.SkillData;
            do
            {
                int targetsLen;
                int damage;
                if((damage = (int)(characterStatus.baseDamage * skillData.damageRatio)) > 0)
                {
                    if(skillData.attackTargets != null && (targetsLen = skillData.attackTargets.Length) > 0)
                    {
                        for(int i = targetsLen - 1; i >= 0; i--)
                        {
                            var targetStatus = skillData.attackTargets[i].GetComponent<CharacterStatus>();
                            targetStatus.OnDamage(damage);
                            var targetFxTF = targetStatus.hitFxTF;
                            var hitFx = GameObjectPool.Instance.CreateObject(
                                skillData.hitFxName, skillData.hitFxPrefab, targetFxTF.position, targetFxTF.rotation, targetFxTF
                                );
                            GameObjectPool.Instance.CollectObject(hitFx, 0.2f);
                        }
                    }
                }

                yield return Waits.GetWaitForSeconds(skillData.damageInterval);

                attackedTime += skillData.damageInterval;
                skillData.attackTargets = deployer.CalculateTargets();

            }while(attackedTime < skillData.durationTime);
        }
    
    }
}
