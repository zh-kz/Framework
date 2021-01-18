namespace Framework.Skill
{
    /// <summary>
    /// 对目标的影响
    /// </summary>
    public interface ITargetImpact
    {
        /// <summary>
        /// 影响目标
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        void ImpactTarget(SkillDeployer deployer);
    }
}
