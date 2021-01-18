namespace Framework.AI.FSM
{
    /// <summary>
    /// 状态ID，命名规则：进行时；相应状态类：XXXState
    /// </summary>
    public enum FSMStateID
    {
        NullState,      // 未知状态	
        Default,        // 默认状态
        ReturnPrevious, // 返回之前的状态
    }
}
