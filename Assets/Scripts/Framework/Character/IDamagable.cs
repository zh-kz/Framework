namespace Framework.Character
{
    /// <summary>
    /// 可受到伤害
    /// </summary>
    public interface IDamagable
    {
        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="value">伤害值</param>
        void OnDamage(int damage);
    }
}
