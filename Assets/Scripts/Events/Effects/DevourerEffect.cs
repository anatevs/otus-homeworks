using UnityEngine;

public struct DevourerEffect : IAttackEffect
{
    public HeroEntity Hero { get; set; }

    public HeroEntity Target { get; set; }


    [field: SerializeField]
    public int ExtraDamage { get; private set; }
}