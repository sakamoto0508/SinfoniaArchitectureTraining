namespace Domain
{
    /// <summary>
    ///     体力の現在値や最大値を管理するエンティティクラス。
    /// </summary>
    public class HealthEntity
    {
        public HealthEntity(float health)
        {
            CurrentHealth = new(health);
            MaxHealth = new(health);
        }

        /// <summary>
        ///     ダメージを適用する。
        /// </summary>
        public void ApplyDamage(float damage)
        {
            if (damage <= 0f) return;
            var newHp = CurrentHealth.Value - damage;
            if (newHp < 0f) newHp = 0f;
            CurrentHealth = new HealthValueObject(newHp);
        }

        public HealthValueObject CurrentHealth { get; private set; }
        public readonly HealthValueObject MaxHealth;
    }
}
