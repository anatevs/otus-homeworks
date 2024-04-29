public readonly struct NextMoveEvent : IEvent
{
    public readonly HeroEntity prevHero;

    public NextMoveEvent(HeroEntity initPrevHero)
    {
        prevHero = initPrevHero;
    }
}