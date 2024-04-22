public readonly struct DefaultDamageEvent : IEvent
{
    public readonly HeroEntity hero;
    public readonly HeroEntity target;

    public DefaultDamageEvent(HeroEntity initHero, HeroEntity initTarget)
    {
        hero = initHero;
        target = initTarget;
    }
}