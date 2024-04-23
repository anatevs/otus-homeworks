using UnityEngine;
public struct ElectroEffect : IDefenceEffect
{
    public int Damage { get; set; }
    public HeroEntity Target { get; set; }

    [field: SerializeField]
    public int extraDamage;
}