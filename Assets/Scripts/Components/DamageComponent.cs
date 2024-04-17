public readonly struct DamageComponent : IComponent
{
    public readonly int value;

    public DamageComponent(int initValue)
    {
        value = initValue;
    }
}