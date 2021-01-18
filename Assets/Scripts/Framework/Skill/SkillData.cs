using UnityEngine;

namespace Framework.Skill
{
    /// <summary>
    /// 技能数据，考虑用ScriptableObject
    /// </summary>
    [System.Serializable]
    public class SkillData
    {
        ///<summary>技能ID</summary>
        public SkillID id;
        ///<summary>技能名称</summary>
        public string name;
        ///<summary>技能描述</summary>
        public string description;
        ///<summary>冷却时间</summary>
        public int coolTime;
        ///<summary>冷却剩余</summary>
        public int coolRemain;
        ///<summary>魔法消耗</summary>
        public int costMana;
        ///<summary>攻击距离</summary>
        public float attackDistance;
        ///<summary>攻击角度</summary> 
        public float attackAngle;
        ///<summary>攻击目标tags</summary> 
        public string[] attackTargetTags = {
            "Enemy","Boss"
        };
        ///<summary>攻击目标对象数组</summary>
        [HideInInspector]
        public Transform[] attackTargets;
        ///<summary>连击的下一个技能id</summary>
        public SkillID nextBatterId;
        ///<summary>伤害比率</summary>
        public float damageRatio;                  //attackRatio  (Character有baseAtk或damage)
        ///<summary>持续时间</summary>
        [Tooltip("持续伤害")]
        public float durationTime;
        ///<summary>伤害间隔</summary>
        [Tooltip("持续伤害")]
        public float damageInterval;
        ///<summary>技能所属</summary>
        [HideInInspector]
        public GameObject owner;
        ///<summary>技能预制件名称</summary>
        public string prefabName;
        ///<summary>技能预制件对象(有释放器)</summary>
        [HideInInspector]
        public GameObject skillPrefab;            
        ///<summary>动画名称</summary> 
        public string animationName;               
        [HideInInspector]
        public int hashAnimationName;
        ///<summary>受击特效名称</summary>
        public string hitFxName;
        ///<summary>受击特效预制件</summary>
        [HideInInspector]
        public GameObject hitFxPrefab;
        ///<summary>技能等级</summary>
        public int level;
        ///<summary>是否激活</summary>
        public bool isAtivated;
        ///<summary>攻击类型:单体，群体</summary> 
        public SkillAttackType attackType;
        ///<summary>选择器类型:圆形，扇形，矩形</summary>  
        public SkillSelectorType selectorType;
        ///<summary>对自身的一系列影响</summary>
        public SelfImpactType[] selfImpactTypes;
        ///<summary>对目标的一系列影响</summary>
        public TargetImpactType[] targetImpactTypes;
    }
}
