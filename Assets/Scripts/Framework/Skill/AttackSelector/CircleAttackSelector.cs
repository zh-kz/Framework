using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Character;

namespace Framework.Skill
{
    /// <summary>
    /// 圆形选择器
    /// </summary>
    public class CircleAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            int tagsLen = skillData.attackTargetTags.Length;
            List<GameObject> allTargets = new List<GameObject>(tagsLen);
            
            for(int i = tagsLen - 1; i >= 0; i--)
            {
                var targets = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                if(targets != null && targets.Length > 0)
                {
                    allTargets.AddRange(targets);
                }
            }
            
            int allCount;
            if((allCount = allTargets.Count) == 0)
            {
                return null;
            }
            
            var dis2 = skillData.attackDistance * skillData.attackDistance;
            var realTargets = new List<Transform>(allCount);
            for(int i = allCount - 1; i >= 0; i--)
            {
                var t = allTargets[i];
                if((t.transform.position - skillTF.position).sqrMagnitude < dis2      
                    && t.GetComponent<CharacterStatus>().curBaseHealth > 0)                         
                {
                    realTargets.Add(t.transform);
                }
            }

            return realTargets.ToArray();
        }
    }
}
