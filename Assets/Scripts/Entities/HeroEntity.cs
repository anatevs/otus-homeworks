using System.Collections;
using UnityEngine;

public class HeroEntity : Entity
{
    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _damage;

    private void Awake()
    {
        Add(new HPComponent(_hp));
        Add(new DamageComponent(_damage));
    }
}