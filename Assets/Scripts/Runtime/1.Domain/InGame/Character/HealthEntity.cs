namespace Domain
{
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
