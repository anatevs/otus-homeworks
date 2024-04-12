public readonly struct AttackEvent
{
    public readonly Entity target;
    public readonly int damage;

    public AttackEvent(Entity target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }
}