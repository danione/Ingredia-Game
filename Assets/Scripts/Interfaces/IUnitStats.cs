public interface IUnitStats
{
    public float Health { get; }
    public abstract void Die();
    public abstract void TakeDamage(float amount);
    public abstract void Heal();
}
