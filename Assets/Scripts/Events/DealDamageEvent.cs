public readonly struct DealDamageEvent
{
    public readonly int damage;
    public readonly HeroEntity entity;

    public DealDamageEvent(HeroEntity entity, int damage)
    {
        this.entity = entity;
        this.damage = damage;
    }
}