namespace Domain
{
    public class HealthEntity
    {
        public HealthEntity(float health)
        {
            CurrentHealth = new(health);
            MaxHealth = new(health);
        }
    }
}
