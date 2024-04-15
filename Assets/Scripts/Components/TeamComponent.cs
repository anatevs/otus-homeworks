public readonly struct TeamComponent : IComponent
{
    public readonly Team value;

    public TeamComponent(Team value)
    {
        this.value = value;
    }
}