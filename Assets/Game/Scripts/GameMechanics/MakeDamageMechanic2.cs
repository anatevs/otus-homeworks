using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageMechanic2
{
    private IAtomicEvent<int> _damagableOnDamage;
    private Entity _damagableEntity;
    private IAtomicEvent _makeDamageEvent;
    private IAtomicValue<int> _damage;

    public MakeDamageMechanic2(Entity damagableEntity, IAtomicEvent makeDamageEvent, IAtomicValue<int> damage)
    {
        _damagableEntity = damagableEntity;
        _makeDamageEvent = makeDamageEvent;
        _damage = damage;
    }

    public void OnEnable()
    {
        _makeDamageEvent.Subscribe(MakeDamage);
    }

    public void OnDisable()
    {
        _makeDamageEvent.Unsubscribe(MakeDamage);
    }

    private void MakeDamage()
    {
        _damagableEntity.GetComponentFromEntity<OnDamageComponent>().MakeDamage(_damage.Value);
    }
}