public readonly struct AttackEvent : IEvent
{
    public readonly HeroEntity target;
    public readonly HeroEntity hero;

    public AttackEvent(HeroEntity initTarget, HeroEntity initHero)
    {
        target = initTarget;
        hero = initHero;
    }
}