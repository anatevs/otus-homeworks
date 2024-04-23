public readonly struct DefaultDealDamageEvent : IEvent
{
    public readonly int damage;
    public readonly HeroEntity entity;

    public DefaultDealDamageEvent(HeroEntity entity, int damage)
    {
        this.entity = entity;
        this.damage = damage;
    }
}