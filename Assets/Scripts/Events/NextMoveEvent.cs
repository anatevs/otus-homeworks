public readonly struct NextMoveEvent
{
    public readonly HeroEntity prevPlayer;

    public NextMoveEvent(HeroEntity prevPlayer)
    {
        this.prevPlayer = prevPlayer;
    }
}