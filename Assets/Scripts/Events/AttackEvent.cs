public readonly struct AttackEvent : IEvent
{
    public readonly int damage;
    public readonly Entity target;

    public AttackEvent(Entity target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }
}