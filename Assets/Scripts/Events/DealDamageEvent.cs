public readonly struct DealDamageEvent
{
    public readonly int damage;
    public readonly Entity entity;

    public DealDamageEvent(Entity entity, int damage)
    {
        this.entity = entity;
        this.damage = damage;
    }
}