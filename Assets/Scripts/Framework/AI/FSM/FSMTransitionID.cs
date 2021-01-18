namespace Framework.AI.FSM
{
    /// <summary>
    /// 转换ID，命名规则：过去时；相应转换类：XXXTransition
    /// </summary>
    public enum FSMTransitionID
    {
        NullTransition, // 未知的转换
        Directly,       //直接转换，自然过渡
    }
}
