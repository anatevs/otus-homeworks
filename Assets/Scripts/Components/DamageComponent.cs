public readonly struct DamageComponent : IComponent
{
    public ComponentName Name => ComponentName.Damage;

    public readonly int value;

    public DamageComponent(int value)
    {
        this.value = value;
    }
}