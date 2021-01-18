using System;
using System.Collections.Generic;
using UnityEngine;
using Framework.Character;

namespace Framework.AI.FSM
{
    /// <summary>
    /// 状态基类，所有状态都需要继承于此
    /// 保存当前状态以及由 转换条件 => 下一个状态的map
    /// </summary>
    public abstract class FSMState
    {
        /// <summary>
        /// 转换条件对象列表（统一当成基类FSMTransition保存）
        /// </summary>
        protected List<FSMTransition> transitions = new List<FSMTransition>();

        /// <summary>
        /// 转换条件->下一个状态ID的映射表
        /// </summary>
        protected Dictionary<FSMTransitionID, FSMStateID> map = new Dictionary<FSMTransitionID, FSMStateID>();
        
        protected FSMStateID stateID;
        /// <summary>
        /// 此状态ID
        /// </summary>
        public FSMStateID StateID { get { return stateID; } }

        /// <summary>
        /// 所属NPC
        /// </summary>
        protected NPC nPC;

        /// <summary>
        /// 默认具有Act方法，但是有些状态不需要不断执行（比如使用协程）就可以不实现
        /// </summary>
        public bool hasAct = true;

        // /// <summary>
        // /// 是否为临时中断状态（不会一直持续），是则需要完成后返回上一个保存的状态
        // /// </summary>
        // public bool isTempState = false;

        /// <summary>
        /// 状态是否已经完成，用来区分是否需要返回的状态（不用返回的状态以进入就标记为完成,在离开状态时标记为false）
        /// </summary>
        public bool isFinished = false;

        protected FSMState(NPC nPC)
        {
            this.nPC = nPC;
        }
    
        /// <summary>
        /// 增加映射对
        /// </summary>
        /// <param name="transitionID">转换条件ID</param>
        /// <param name="stateID">下一个转台ID</param>
        public void AddTransition(FSMTransitionID transitionID, FSMStateID stateID)
        {
            // 判空
            if (transitionID == FSMTransitionID.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }
    
            if (stateID == FSMStateID.NullState)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }
    
            // 已经存在
            if (map.ContainsKey(transitionID))
            {
                Debug.LogError("FSMState ERROR: State " + this.stateID.ToString() + " already has transition " + transitionID.ToString() + 
                            "Impossible to assign to another state");
                return;
            }
    
            map.Add(transitionID, stateID);
            AddTransitionObject(transitionID);
        }

        /// <summary>
        /// 添加转换条件对象
        /// </summary>
        /// <param name="transitionID"></param>
        private void AddTransitionObject(FSMTransitionID transitionID)
        {
            //反射，命名规则：所有Transition都以对应的TransitionID+Transition命名
            Type type = Type.GetType("Mystery.FSM." + transitionID + "Transition");
            if(type != null)
            {
                System.Object[] param = new System.Object[1]{nPC};
                FSMTransition transitionObj = Activator.CreateInstance(type, param) as FSMTransition;
                transitions.Add(transitionObj);
            }
        }
    
        /// <summary>
        /// 删除映射对
        /// </summary>
        public void RemoveTransition(FSMTransitionID transitionID)
        {
            // 判空
            if (transitionID == FSMTransitionID.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }
    
            // 确实存在
            if (map.ContainsKey(transitionID))
            {
                map.Remove(transitionID);
                RemoveTransitionObject(transitionID);
                return;
            }
            Debug.LogError("FSMState ERROR: Transition " + transitionID.ToString() + " passed to " + stateID.ToString() + 
                        " was not on the state's transition list");
        }

        /// <summary>
        /// 删除转换条件对象
        /// </summary>
        /// <param name="transitionID"></param>
        private void RemoveTransitionObject(FSMTransitionID transitionID)
        {
            transitions.RemoveAll(t => t.TransitionID == transitionID);
        }
    
        /// <summary>
        /// 根据转换条件获得下一个状态ID
        /// </summary>
        /// <param name="transitionID"></param>
        /// <returns>不存在转换映射将返回NUllStateID</returns>
        public FSMStateID GetOutputState(FSMTransitionID transitionID)
        {
            // 存在转换映射
            if (map.ContainsKey(transitionID))
            {
                return map[transitionID];
            }
            return FSMStateID.NullState;
        }
    
        /// <summary>
        /// 进入当前状态之前由FSM系统自动调用
        /// </summary>
        public abstract void DoBeforeEntering();
    
        /// <summary>
        /// 离开当前状态之前由FSM系统自动调用
        /// </summary>
        public abstract void DoBeforeLeaving();

        /// <summary>
        /// 判断所有转换条件，满足就会调用转换
        /// </summary>
        public virtual void Reason()
        {
            int count = transitions.Count;
            for(int i = 0; i < count; i++)
            {
                if(transitions[i].HandleTransition())
                {
                    nPC.PerformTransition(transitions[i].TransitionID);
                    return;
                }
            }
        }
        
        /// <summary>
        /// NPC当前状态下的行为
        /// </summary>
        public virtual void Act() { }

    }
}
