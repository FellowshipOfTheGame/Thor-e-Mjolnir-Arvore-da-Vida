namespace ThorGame
{
    public interface IHealthProvider
    {
        delegate void HealthUpdateEvent(int oldHealth, int newHealth);
        event HealthUpdateEvent OnHealthChanged;

        int MaxHealth { get; }
        int Health { get; }
    }
}