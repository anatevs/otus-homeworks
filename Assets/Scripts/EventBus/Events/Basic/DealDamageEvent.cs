public readonly struct DealDamageEvent : IEvent
{
    public readonly int damage;
    public readonly HeroEntity target;

    public DealDamageEvent(HeroEntity initTarget, int initDamage)
    {
        target = initTarget;
        damage = initDamage;
    }
}