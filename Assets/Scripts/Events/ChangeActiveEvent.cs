public readonly struct ChangeActiveEvent : IEvent
{
    public readonly HeroEntity prevHero;
    public readonly HeroEntity currentHero;

    public ChangeActiveEvent(HeroEntity prevHero, HeroEntity currentHero)
    {
        this.prevHero = prevHero;
        this.currentHero = currentHero;
    }
}