public readonly struct WeaponComponent : IComponent
{
    public readonly IAttackEffect effect;

    public WeaponComponent(IAttackEffect initEffect)
    {
        effect = initEffect;
    }
}