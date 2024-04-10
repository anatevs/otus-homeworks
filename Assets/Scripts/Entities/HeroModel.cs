using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroModel : EntityProvider
{
    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _damage;

    private void Awake()
    {
        Init(new Entity());

        Entity.Add(new HPComponent(_hp));
        Entity.Add(new DamageComponent(_damage));
    }
}