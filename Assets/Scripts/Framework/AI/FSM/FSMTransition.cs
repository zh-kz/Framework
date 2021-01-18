using Framework.Character;

namespace Framework.AI.FSM
{
    /// <summary>
    /// 转换条件的基类
    /// </summary>
    public abstract class FSMTransition
    {
        protected FSMTransitionID transitionID;
        /// <summary>
        /// 此转换ID
        /// </summary>
        /// <value></value>
        public FSMTransitionID TransitionID { get { return transitionID; }}

        /// <summary>
        /// 所属NPC
        /// </summary>
        protected NPC nPC;

        public FSMTransition(NPC nPC)
        {
            this.nPC = nPC;
        }

        /// <summary>
        /// 检查转换条件
        /// </summary>
        /// <returns>是否满足转换条件</returns>
        public abstract bool HandleTransition();
    }
}
