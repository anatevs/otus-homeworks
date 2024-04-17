public readonly struct TeamComponent : IComponent
{
    public readonly Team value;

    public TeamComponent(Team initValue)
    {
        value = initValue;
    }
}