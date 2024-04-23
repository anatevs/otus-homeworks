using UnityEngine;

public struct PaladinEffect : IDefenceEffect
{
    public HeroEntity Target { get; set; }

    public int Damage { get; set; }

    [field: SerializeField]
    public bool IsFirstDefence { get; set; }
}