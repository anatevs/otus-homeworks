public readonly struct DefaultAttackEvent : IEvent
{
    public readonly HeroEntity hero;
    public readonly HeroEntity target;

    public DefaultAttackEvent(HeroEntity initHero, HeroEntity initTarget)
    {
        hero = initHero;
        target = initTarget;
    }
}