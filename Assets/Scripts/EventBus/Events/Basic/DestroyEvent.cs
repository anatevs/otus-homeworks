public readonly struct DestroyEvent : IEvent
{
    public readonly HeroEntity entity;

    public DestroyEvent(HeroEntity initEntity)
    {
        entity = initEntity;
    }
}