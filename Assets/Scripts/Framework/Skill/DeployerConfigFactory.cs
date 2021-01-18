using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = System.Object;   

namespace Framework.Skill
{
    /// <summary>
    /// 释放器工厂
    /// </summary>
    public static class DeployerConfigFactory
    {
        /// <summary>
        /// 反射对象的缓存:类名-对象
        /// </summary>
        private static Dictionary<string, Object> cache;

        /// <summary>
        /// 当前命名空间.
        /// </summary>
        /// <returns></returns>
        private static readonly string curNamespaceDot = 
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + '.';

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, Object>();
        }

        /// <summary>
        /// 根据技能数据创建攻击选择器
        /// </summary>
        /// <param name="skillData">技能数据</param>
        /// <returns>攻击选择器</returns>
        public static IAttackSelector CreateAttackSelector(SkillData skillData)
        {
            string className = curNamespaceDot + skillData.selectorType.ToString() + "AttackSelector";
            
            return CreateObject<IAttackSelector>(className);
        }

        /// <summary>
        /// 创建自身影响
        /// </summary>
        /// <param name="skillData">技能数据</param>
        /// <returns></returns>
        public static List<ISelfImpact> CreateSelfImpacts(SkillData skillData)
        {
            List<ISelfImpact> selfImpacts = new List<ISelfImpact>(skillData.selfImpactTypes.Length);
            foreach(var selfImpactType in skillData.selfImpactTypes)
            {
                string className = curNamespaceDot + selfImpactType.ToString() + "SelfImpact";
                selfImpacts.Add(CreateObject<ISelfImpact>(className));
            }

            return selfImpacts;
        }

        /// <summary>
        /// 创建目标影响
        /// </summary>
        /// <param name="skillData">技能数据</param>
        /// <returns></returns>
        public static List<ITargetImpact> CreateTargetImpacts(SkillData skillData)
        {
            List<ITargetImpact> targetImpacts = new List<ITargetImpact>(skillData.targetImpactTypes.Length);
            foreach(var targetImpactType in skillData.targetImpactTypes)
            {
                string className = curNamespaceDot + targetImpactType.ToString() + "TargetImpact";
                targetImpacts.Add(CreateObject<ITargetImpact>(className));
            }

            return targetImpacts;
        }       

        /// <summary>
        /// 使用缓存的反射
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        private static T CreateObject<T>(string className) where T : class
        {
            Object obj;
            if(!cache.TryGetValue(className, out obj))   
            {                                            
                Type type = Type.GetType(className);
                obj = Activator.CreateInstance(type);
                if(obj == null)
                {
                    Debug.LogError("Activator failed to create: " + className);
                }
                cache.Add(className, obj);
            }

            return obj as T;
        }
    }
}
