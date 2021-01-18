using UnityEngine;
using Framework.Character;

namespace Framework.AI.FSM
{
    /// <summary>
    /// 返回上一个翻转状态，即使没有后续状态，也需要写入状态配置表里（单独一个空字典）
    /// </summary>
    public class ReturnPreviousState : FSMState
    {
        public ReturnPreviousState(NPC nPC): base(nPC)
        {
            stateID = FSMStateID.ReturnPrevious;
            hasAct = false;
        }

        public override void DoBeforeEntering()
        {
            Debug.Log("ReturnPreviousState");
            isFinished = false;
            nPC.ReturnPreviousState();
        }

        public override void DoBeforeLeaving()
        {
            isFinished = true;
        }
    }
}
