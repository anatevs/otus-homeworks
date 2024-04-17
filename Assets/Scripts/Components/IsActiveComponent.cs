public readonly struct IsActiveComponent : IComponent
{
    public readonly bool value;

    public IsActiveComponent(bool initValue)
    {
        value = initValue;
    }
}