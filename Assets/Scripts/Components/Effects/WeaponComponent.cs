public readonly struct WeaponComponent : IComponent
{
    public readonly Ability ability;

    public WeaponComponent(Ability initAbility)
    {
        ability = initAbility;
    }
}