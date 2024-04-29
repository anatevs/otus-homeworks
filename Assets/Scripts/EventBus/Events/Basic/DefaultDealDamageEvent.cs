public readonly struct DefaultDealDamageEvent : IEvent
{
    public readonly int damage;
    public readonly HeroEntity entity;

    public DefaultDealDamageEvent(HeroEntity initEntity, int initDamage)
    {
        entity = initEntity;
        damage = initDamage;
    }
}