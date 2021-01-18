using UnityEngine;
using Framework.Character;

namespace Framework.AI.FSM
{
    /// <summary>
    /// 直接转换
    /// </summary>
    public class DirectlyTransition : FSMTransition
    {
        public DirectlyTransition(NPC nPC): base(nPC)
        {
            transitionID = FSMTransitionID.Directly;
        }

        public override bool HandleTransition()
        {
            return true;
        }
    }
}
