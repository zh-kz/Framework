namespace Framework.Skill
{
    /// <summary>
    /// 对自身影响 
    /// </summary>
    public interface ISelfImpact
    {
        /// <summary>
        /// 影响自身
        /// </summary>
        /// <param name="deployer">技能释放器</param>
        void ImpactSelf(SkillDeployer deployer);
    }
}
