public readonly struct DestroyEvent
{
    public readonly HeroEntity entity;

    public DestroyEvent(HeroEntity entity)
    {
        this.entity = entity;
    }
}