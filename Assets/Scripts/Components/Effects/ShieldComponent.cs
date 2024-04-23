public readonly struct ShieldComponent : IComponent
{
    public readonly IDefenceEffect effect;

    public ShieldComponent(IDefenceEffect initEffect)
    {
        effect = initEffect;
    }
}