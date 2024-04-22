public struct LordVampEffect : IEffect
{
    public AbilityType AbilityType { get => AbilityType.Weapon; }
    public HeroEntity Hero { get; set; }
    public HeroEntity Target { get; set; }
}