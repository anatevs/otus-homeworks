public readonly struct HPComponent : IComponent
{
    public ComponentName Name => ComponentName.HP;

    public readonly int value;

    public HPComponent(int value)
    {
        this.value = value;
    }
}