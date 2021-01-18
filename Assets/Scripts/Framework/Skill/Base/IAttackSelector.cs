using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Skill
{
    /// <summary>
    /// 攻击选择器，提供选择目标算法
    /// </summary>
    public interface IAttackSelector        //命名规则：XXXAttackSelector
    {
        /// <summary>
        /// 返回选择目标
        /// </summary>
        /// <param name="skillData">技能数据</param>
        /// <param name="skillTF">技能所在对象的TF</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData skillData, Transform skillTF);
    }
}
