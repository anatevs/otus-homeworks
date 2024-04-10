public readonly struct TeamComponent : IComponent
{
    public ComponentName Name => ComponentName.Team;

    public readonly Team value;

    public TeamComponent(Team value)
    {
        this.value = value;
    }
}