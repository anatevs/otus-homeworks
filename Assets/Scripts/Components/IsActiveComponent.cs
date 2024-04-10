public readonly struct IsActiveComponent : IComponent
{
    public ComponentName Name => ComponentName.IsActive;

    public readonly bool value;

    public IsActiveComponent(bool value)
    {
        this.value = value;
    }
}