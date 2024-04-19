public readonly struct NextMoveEvent : IEvent
{
    public readonly HeroEntity prevPlayer;

    public NextMoveEvent(HeroEntity prevPlayer)
    {
        this.prevPlayer = prevPlayer;
    }
}