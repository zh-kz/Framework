using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Framework.AI.FSM;

namespace Framework.Character
{
    public class NPC : CachedBehaviour
    {
        public int id;
        public string nPCName;
        private NavMeshAgent agent;
        private Animator animator;

        private FSMSystem fSMSystem;
        [SerializeField]
        private FSMStateID defaultStateID;
        private static readonly string[] configFiles = {
            "FSM_00.json"
        };

#region 转换标志

        [HideInInspector]
        public bool isDirectly;
            
#endregion 转换标志

#region 协程状态标志
    


#endregion 协程状态标志

#region DEBUG

        [SerializeField]
        private FSMStateID debugCurState;

#endregion DEBUG

#region 生命周期
    
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            MakeFSM(configFiles[0]);
        }

#endregion 生命周期

#region FSM

        /// <summary>
        /// 执行转换，fSMSystem.PerformTransition
        /// </summary>
        /// <param name="transitionID"></param>
        public void PerformTransition(FSMTransitionID transitionID)
        {
            fSMSystem.PerformTransition(transitionID);
        }

        /// <summary>
        /// 返回上一个需要翻转的状态
        /// </summary>
        public void ReturnPreviousState()
        {
            fSMSystem.ReturnPreviousState();
        }

        /// <summary>
        /// reason, act
        /// </summary>
        public void UpdateState()
        {
            fSMSystem.CurrentState.Reason();
            if(fSMSystem.CurrentState.hasAct)
            {
                fSMSystem.CurrentState.Act();
            }
            
            debugCurState = fSMSystem.CurrentState.StateID;
        }

        /// <summary>
        /// 建立FSMSystem
        /// </summary>
        private void MakeFSM(string filename)
        {
            fSMSystem = new FSMSystem(this);
            //配置
            fSMSystem.ConfigFSM(filename);
            //初始化
            fSMSystem.InitDefaultState(defaultStateID);
        }

#endregion FSM

    }
}
