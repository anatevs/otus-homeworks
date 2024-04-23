public readonly struct NextMoveEvent : IEvent
{
    public readonly HeroEntity prevHero;

    public NextMoveEvent(HeroEntity prevHero)
    {
        this.prevHero = prevHero;
    }
}