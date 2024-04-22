using UnityEngine;

public struct DevourerEffect : IEffect
{
    public AbilityType AbilityType { get => AbilityType.Weapon; }
    public HeroEntity Hero { get; set; }
    public HeroEntity Target { get; set; }

    [field: SerializeField]
    public int ExtraDamage { get; private set; }
}