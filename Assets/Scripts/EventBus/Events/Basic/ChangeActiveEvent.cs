public readonly struct ChangeActiveEvent : IEvent
{
    public readonly HeroEntity prevHero;
    public readonly HeroEntity currentHero;

    public ChangeActiveEvent(HeroEntity initPrevHero, HeroEntity initCurrentHero)
    {
        prevHero = initPrevHero;
        currentHero = initCurrentHero;
    }
}