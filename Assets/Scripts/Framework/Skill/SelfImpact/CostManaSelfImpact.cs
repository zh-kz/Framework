using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Character;

namespace Framework.Skill
{
    /// <summary>
    /// 消耗魔法值
    /// </summary>
    public class CostManaSelfImpact : ISelfImpact
    {
        public void ImpactSelf(SkillDeployer deployer)
        {
            GameObject owner;
            if((owner = deployer.SkillData.owner) != null)
            {
                //应该也可以提前缓存
                owner.GetComponent<CharacterStatus>().curBaseMana
                    -= deployer.SkillData.costMana;
            }
        }
    }
}
