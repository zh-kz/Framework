using System;
using System.Collections.Generic;
using UnityEngine;
using Framework.Character;

namespace Framework.AI.FSM
{
    /// <summary>
    /// 保存NPC具有的所有状态，作为NPC类的变量
    /// 提供添加/删除状态的方法，执行状态转换的方法
    /// </summary>
    public class FSMSystem
    {
        /// 扩展状态和转换:
        /// 1.先补充相应枚举名字
        /// 2.新状态/转换类必须遵循命名规则（直接复制再加后缀），必须在Framework.AI.FSM下
        /// 3.考虑原有的转换图，完善新的配置表（同样注意命名）

        /// <summary>
        /// 此对象的所有状态
        /// </summary>
        private List<FSMState> states;
    
        private FSMStateID currentStateID;
        /// <summary>
        /// 当前状态ID
        /// </summary>
        /// <value></value>
        public FSMStateID CurrentStateID { get { return currentStateID; } }
        
        private FSMState currentState;
        /// <summary>
        /// 当前状态
        /// </summary>
        /// <value></value>
        public FSMState CurrentState { get { return currentState; } }

        /// <summary>
        /// 需要状态翻转的上一个状态ID
        /// </summary>
        private FSMStateID previousStateID;

        /// <summary>
        /// 缓存的状态翻转链
        /// </summary>
        private Stack<FSMState> previousStates;

    
        /// <summary>
        /// 默认状态
        /// </summary>
        private FSMState defaultState;
        public FSMStateID DefaultStateID;

        /// <summary>
        /// 所属NPC(用于反射状态)
        /// </summary>
        private NPC nPC;

        /// <summary>
        /// 当前命名空间.
        /// </summary>
        /// <returns></returns>
        private static readonly string curNamespaceDot = 
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + '.';
        
        public FSMSystem(NPC nPC)
        {
            this.nPC = nPC;
            states = new List<FSMState>();
            
            previousStates = new Stack<FSMState>();
        }

        /// <summary>
        /// 配置，添加状态和转换
        /// </summary>
        public void ConfigFSM(string filename)
        {
            //反射
            System.Object[] param = new System.Object[1]{nPC};
            var map = FSMConfigurationReader.ReadCongfig(filename);
            List<FSMStateID> anyStateNextStates = new List<FSMStateID>();
            List<FSMTransitionID> anyStateTrans = new List<FSMTransitionID>();
            int anyTransCount = 0;
            foreach(string stateID in map.Keys)
            {
                if(stateID == "Any")
                {
                    // 特殊处理
                    // 每个状态都要添加这个"Any"下的所有转换（需要写在配置表开头），同样不需要状态实体
                    // 同时可能需要添加NPC的状态变量如hp
                    foreach(string transitionID in map[stateID].Keys)
                    {
                        FSMTransitionID t = (FSMTransitionID)Enum.Parse(typeof(FSMTransitionID), transitionID);
                        FSMStateID s = (FSMStateID)Enum.Parse(typeof(FSMStateID), map[stateID][transitionID]);
                        anyStateNextStates.Add(s);
                        anyStateTrans.Add(t);
                    }
                    anyTransCount = anyStateTrans.Count;
                    continue;
                }
                Type type = Type.GetType(curNamespaceDot + stateID + "State");
                FSMState state = Activator.CreateInstance(type, param) as FSMState;
                AddState(state);
                // 先添加anystate的转换
                for(int i = 0; i < anyTransCount; i++)
                {
                    state.AddTransition(anyStateTrans[i], anyStateNextStates[i]);
                }
                // 再添加这个状态的
                foreach(string transitionID in map[stateID].Keys)
                {
                    FSMTransitionID t = (FSMTransitionID)Enum.Parse(typeof(FSMTransitionID), transitionID);
                    FSMStateID s = (FSMStateID)Enum.Parse(typeof(FSMStateID), map[stateID][transitionID]);
                    state.AddTransition(t, s);
                }
            }
        }

        /// <summary>
        /// 初始化（进入）默认状态
        /// </summary>
        public void InitDefaultState(FSMStateID defaultStateID)
        {
            //将DefaultStateID对应的状态设为默认状态（别名）
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentStateID = defaultStateID;
            currentState.DoBeforeEntering();
        }
    
        /// <summary>
        /// 添加状态
        /// </summary>
        public void AddState(FSMState s)
        {
            // 判空
            if (s == null)
            {
                Debug.LogError("AddState: null");
                return;
            }
    
            // 已经存在
            if(states.Find(state => state.StateID == s.StateID) != null)
            {
                Debug.LogError(s.StateID.ToString() + "已经存在");
                return;
            }

            states.Add(s);
        }
    
        /// <summary>
        /// 删除ID对应的状态
        /// </summary>
        public void RemoveState(FSMStateID id)
        {
            // 判空
            if (id == FSMStateID.NullState)
            {
                Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
                return;
            }
    
            FSMState state = states.Find(s => s.StateID == id);
            if(state != null)
            {
                states.Remove(state);
            }
            else
            {
                Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() + 
                        ". It was not on the list of states");
            }
        }
    
        /// <summary>
        /// 执行状态转换，当前+条件->下一个
        /// </summary>
        public void PerformTransition(FSMTransitionID trans)
        {
            // 判空
            if (trans == FSMTransitionID.NullTransition)
            {
                Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
                return;
            }
    
            // 尝试获取下一个状态ID
            FSMStateID nextStateID = currentState.GetOutputState(trans);
            if (nextStateID == FSMStateID.NullState)
            {
                Debug.LogError("FSM ERROR: State " + currentStateID.ToString() +  " does not have a target state " + 
                            " for transition " + trans.ToString());
                return;
            }

            // 获取下一个状态
            FSMState nextState = null;
            if(nextStateID == FSMStateID.Default)
            {
                nextState = defaultState;
            }
            else
            {
                nextState = states.Find(s => s.StateID == nextStateID);
            }

            // 更新状态	
            currentState.DoBeforeLeaving();

            if(!currentState.isFinished)
            {
                SavePreviousState();
            }

            currentState = nextState;
            currentStateID =  nextState.StateID;
            currentState.DoBeforeEntering();
    
        }

        /// <summary>
        /// 保存当前状态为需要翻转的状态  
        /// </summary>
        public void SavePreviousState()
        {
            previousStates.Push(currentState);
            Debug.Log("Add previous state: " + currentState.StateID.ToString());
        }

        /// <summary>
        /// 返回上一个需要翻转的状态
        /// </summary>
        public void ReturnPreviousState()
        {
            if(previousStates.Count > 0)
            {
                FSMState state;
                do
                {
                    state = previousStates.Pop();
                } while (state.isFinished);     //保证不会退回到已经完成的状态(一般需要通过FinishState来手动设置)

                if(state.StateID != currentStateID)
                {
                    currentState.DoBeforeLeaving();
                    currentState = state;
                    currentStateID = state.StateID;
                    currentState.DoBeforeEntering();
                }
            }
        }

        /// <summary>
        /// 将stateID状态设置为已完成，statid应该是保存在previousStates中的，现在取消这个回退
        /// </summary>
        /// <param name="stateID"></param>
        public void FinishState(FSMStateID stateID)
        {
            FSMState state = states.Find(s => s.StateID == stateID);
            if(state != null)
            {
                state.isFinished = true;
            }
            else
            {
                Debug.LogWarning("Cant't finish state: " + stateID.ToString() + "because can't find it");
            }
        }

    }
}
