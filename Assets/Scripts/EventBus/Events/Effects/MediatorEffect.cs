using System.Collections.Generic;
using UnityEngine;
public struct MediatorEffect : IAttackEffect
{
    public HeroEntity Target { get; set; }

    public HeroEntity Hero { get; set; }


    [field: SerializeField]
    public int extraHP;

    public IReadOnlyList<HeroEntity> TeammateEntities { get; set; }
}