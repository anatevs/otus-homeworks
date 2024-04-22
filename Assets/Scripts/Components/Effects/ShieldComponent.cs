public readonly struct ShieldComponent : IComponent
{
    public readonly Ability ability;

    public ShieldComponent(Ability initAbility)
    {
        ability = initAbility;
    }
}