using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Skill
{
    /// <summary>
    /// 近身释放器
    /// </summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            if(skillData != null)
            {
                skillData.attackTargets = CalculateTargets();
                selfImpacts.ForEach(t => t.ImpactSelf(this));
                targetImpacts.ForEach(t => t.ImpactTarget(this));
                CollectSkill();
            }
        }
    }
}
