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

        public HealthValueObject CurrentHealth { get; private set; }
        public readonly HealthValueObject MaxHealth;
    }
}
